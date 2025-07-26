using CadastroEmpresas.API.Models;
using CadastroEmpresas.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroEmpresas.API.Services
{
    public class TokenService
    {
        private readonly ConfiguracoesToken _config;

        public TokenService(IOptions<ConfiguracoesToken> config)
        {
            _config = config.Value;
        }

        public string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.ChaveSecreta));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.Emissor,
                audience: _config.Audiencia,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_config.ExpiracaoEmMinutos),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
