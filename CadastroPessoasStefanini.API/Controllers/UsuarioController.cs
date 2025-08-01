using CadastroPessoasStefanini.API.JWT;
using CadastroPessoasStefanini.Application;
using CadastroPessoasStefanini.Data.Context;
using CadastroPessoasStefanini.Domain.DTO;
using CadastroPessoasStefanini.Domain.Entities;
using CadastroPessoasStefanini.Util.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoasStefanini.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioHandler _usuarioHandle;
        private readonly TokenService _tokenService;

        public UsuarioController(AppDbContext context, TokenService tokenService)
        {
            _usuarioHandle = new UsuarioHandler(context);
            _tokenService = tokenService;
        }

        /// <summary>
        /// Retorna todos os usuário cadastrados
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet]
        public async Task<ActionResult<Usuario>> Get()
        {
            var usuarios = await _usuarioHandle.GetAll();
            if (usuarios is null) return NoContent();

            return Ok(usuarios);
        }

        /// <summary>
        /// Realiza o login no sistema
        /// </summary>
        /// <param name="login">Dados do Login</param>
        /// <returns>Retorna o token JWT </returns>
        [HttpPost("Login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO login)
        {
            var usuario = await _usuarioHandle.Login(login);
            if (usuario is null) return BadRequest("Usuário inválido");

            var token = _tokenService.GerarToken(usuario.IdUsuario);

            return Ok(new TokenDTO() { Token = token });
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuarioDTO">Dados do usuário</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(UsuarioDTO usuarioDTO)
        {
            try
            {
                await _usuarioHandle.Create(GetUsuario(usuarioDTO));
                return Ok();
            }
            catch (ExceptionValidation ex)
            {
                return BadRequest(ex.Validacoes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Atualiza dados do usuário
        /// </summary>
        /// <param name="id">Id usuário</param>
        /// <param name="usuarioDTO">Dados do usuário</param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UsuarioAtualizacaoDTO usuarioDTO)
        {
            try
            {
                await _usuarioHandle.Update(id, usuarioDTO);
                return Ok();
            }
            catch (ExceptionValidation ex)
            {
                return BadRequest(ex.Validacoes);
            }
            catch (ExceptionNotFound ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Exclui um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _usuarioHandle.Delete(id);
                return Ok();
            }
            catch (ExceptionValidation ex)
            {
                return BadRequest(ex.Validacoes);
            }
            catch (ExceptionNotFound ex)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retorna um objeto Usuario a partir de um UsuarioDTO
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        private Usuario GetUsuario(UsuarioDTO usuarioDTO)
        {
            return new Usuario()
            {
                Email = usuarioDTO.Email,
                Nome = usuarioDTO.Nome,
                Senha = usuarioDTO.Senha
            };
        }
    }
}
