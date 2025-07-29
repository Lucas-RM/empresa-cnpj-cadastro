using CadastroEmpresas.API.DTOs;
using CadastroEmpresas.API.Services;
using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.Domain.Interfaces;
using CadastroEmpresas.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEmpresas.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        private readonly TokenService _tokenService;

        public AuthController(IUsuarioRepository userRepositorio, TokenService tokenService)
        {
            _usuarioRepositorio = userRepositorio;
            _tokenService = tokenService;
        }

        // POST: api/auth/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioCadastroDto usuarioCadastroDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioExistente = await _usuarioRepositorio.GetByEmailAsync(usuarioCadastroDto.Email);
            if (usuarioExistente != null)
            {
                return BadRequest("Já existe um usuário com esse e-mail.");
            }

            if (!EmailUtils.Validar(usuarioCadastroDto.Email))
            {
                return BadRequest("E-mail inválido.");
            }

            var usuario = new Usuario
            {
                Nome = usuarioCadastroDto.Nome,
                Email = usuarioCadastroDto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioCadastroDto.Senha)
            };

            await _usuarioRepositorio.AddAsync(usuario);

            var token = _tokenService.GerarToken(usuario);

            var resposta = new RespostaAutenticacaoDto
            {
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            return Ok(resposta);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioCadastroDto)
        {
            var usuario = await _usuarioRepositorio.GetByEmailAsync(usuarioCadastroDto.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(usuarioCadastroDto.Senha, usuario.SenhaHash))
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var token = _tokenService.GerarToken(usuario);

            var resposta = new RespostaAutenticacaoDto
            {
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            return Ok(resposta);
        }
    }
}
