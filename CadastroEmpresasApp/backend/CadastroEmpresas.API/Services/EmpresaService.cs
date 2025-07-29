using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.Domain.Interfaces;
using CadastroEmpresas.Domain.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace CadastroEmpresas.API.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmpresaService(IEmpresaRepository empresaRepository, IHttpClientFactory httpClientFactory)
        {
            _empresaRepository = empresaRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(bool Sucesso, string Mensagem)> CadastrarEmpresaPorCnpjAsync(string cnpj, Guid usuarioId)
        {
            cnpj = CnpjUtils.Limpar(cnpj);
            if (cnpj.IsNullOrEmpty())
            {
                return (false, "CNPJ inválido ou não encontrado.");
            }

            var empresaExistente = await _empresaRepository.BuscarPorCnpjAsync(cnpj);

            if (empresaExistente != null)
            {
                bool jaVinculada = await _empresaRepository.UsuarioJaCadastrouEmpresa(usuarioId, empresaExistente.Id);

                if (jaVinculada)
                    return (false, "Você já cadastrou essa empresa.");

                await _empresaRepository.VincularEmpresaAoUsuarioAsync(usuarioId, empresaExistente.Id);
                await _empresaRepository.SalvarAsync();
                
                return (true, "Cadastro realizado com sucesso.");
            }

            // Consultar ReceitaWS
            var empresa = await ConsultarReceitaWsAsync(cnpj);
            if (empresa == null)
                return (false, "CNPJ inválido ou não encontrado.");

            empresa.Id = Guid.NewGuid();
            empresa.Cnpj = CnpjUtils.Limpar(empresa.Cnpj);

            await _empresaRepository.CadastrarEmpresaAsync(empresa);
            await _empresaRepository.VincularEmpresaAoUsuarioAsync(usuarioId, empresa.Id);
            await _empresaRepository.SalvarAsync();

            return (true, "Empresa cadastrada e vinculada com sucesso.");
        }

        public async Task<Empresa?> ConsultarReceitaWsAsync(string cnpj)
        {
            var cliente = _httpClientFactory.CreateClient();
            var resposta = await cliente.GetFromJsonAsync<JsonElement>($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");

            if (resposta.TryGetProperty("status", out var status) && status.GetString() == "ERROR")
                return null;

            var empresa = new Empresa
            {
                Cnpj = resposta.GetProperty("cnpj").GetString() ?? string.Empty,
                Nome = resposta.GetProperty("nome").GetString() ?? string.Empty,
                NomeFantasia = resposta.GetProperty("fantasia").GetString() ?? string.Empty,
                Situacao = resposta.GetProperty("situacao").GetString() ?? string.Empty,
                Abertura = resposta.GetProperty("abertura").GetString() ?? string.Empty,
                NaturezaJuridica = resposta.GetProperty("natureza_juridica").GetString() ?? string.Empty,
                AtividadePrincipal = resposta.GetProperty("atividade_principal")[0].GetProperty("text").GetString() ?? string.Empty,
                Endereco = new EnderecoEmpresa
                {
                    Logradouro = resposta.GetProperty("logradouro").GetString() ?? string.Empty,
                    Numero = resposta.GetProperty("numero").GetString() ?? "S/N",
                    Complemento = resposta.GetProperty("complemento").GetString() ?? string.Empty,
                    Bairro = resposta.GetProperty("bairro").GetString() ?? string.Empty,
                    Municipio = resposta.GetProperty("municipio").GetString() ?? string.Empty,
                    UF = resposta.GetProperty("uf").GetString() ?? string.Empty,
                    CEP = resposta.GetProperty("cep").GetString() ?? string.Empty
                }
            };

            return empresa;
        }

        public async Task<List<Empresa>> ListarEmpresasDoUsuarioAsync(Guid usuarioId, int pagina, int tamanho)
        {
            return await _empresaRepository.ListarEmpresasDoUsuarioAsync(usuarioId, pagina, tamanho);
        }
    }
}
