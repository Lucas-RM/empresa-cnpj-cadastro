using CadastroEmpresas.API.DTOs;
using CadastroEmpresas.API.Services;
using CadastroEmpresas.Domain.Interfaces;
using CadastroEmpresas.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CadastroEmpresas.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/empresa")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepository _empresaRepositorio;
        private readonly EmpresaService _empresaService;

        public EmpresaController(IEmpresaRepository empresaRepositorio, EmpresaService empresaService)
        {
            _empresaRepositorio = empresaRepositorio;
            _empresaService = empresaService;
        }

        // POST: api/empresa/cadastrar
        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] EmpresaCadastroDto empresaCadastrodto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuarioId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var resultado = await _empresaService.CadastrarEmpresaPorCnpjAsync(empresaCadastrodto.Cnpj, usuarioId);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Mensagem);

            return Ok(new { mensagem = resultado.Mensagem });
        }

        // GET: api/empresa/listar
        [HttpGet("listar")]
        public async Task<IActionResult> Listar([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var usuarioId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var lista = await _empresaService.ListarEmpresasDoUsuarioAsync(usuarioId, pagina, tamanho);
            var total = await _empresaRepositorio.ContarEmpresasDoUsuarioAsync(usuarioId);

            return Ok(new
            {
                Total = total,
                PaginaAtual = pagina,
                PaginaTamanho = tamanho,
                Dados = lista.Select(e => new EmpresaListagemDto
                {
                    Cnpj = CnpjUtils.Formatar(e.Cnpj),
                    Nome = e.Nome,
                    NomeFantasia = e.NomeFantasia,
                    Situacao = e.Situacao,
                    Abertura = e.Abertura,
                    AtividadePrincipal = e.AtividadePrincipal,
                    Logradouro = e.Endereco.Logradouro,
                    Numero = e.Endereco.Numero,
                    Municipio = e.Endereco.Municipio,
                    UF = e.Endereco.UF,
                    CEP = e.Endereco.CEP,
                })
            });
        }
    }
}
