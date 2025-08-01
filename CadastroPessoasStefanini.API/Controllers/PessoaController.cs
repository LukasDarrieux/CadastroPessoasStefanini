using CadastroPessoasStefanini.Application;
using CadastroPessoasStefanini.Data.Context;
using CadastroPessoasStefanini.Domain.DTO;
using CadastroPessoasStefanini.Domain.Entities;
using CadastroPessoasStefanini.Util.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoasStefanini.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaHandler _pessoaHandle;

        public PessoaController(AppDbContext context)
        {
            _pessoaHandle = new PessoaHandler(context);
        }

        /// <summary>
        /// Retorna todas as pessoas cadastradas
        /// </summary>
        /// <returns>Lista de pessoas</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> Get()
        {
            var pessoas = await _pessoaHandle.GetAll();
            if (pessoas is null) return NoContent();
            
            return Ok(pessoas);
        }

        /// <summary>
        /// Retorna uma pessoa a partir do Id informado
        /// </summary>
        /// <param name="id">Id Pessoa</param>
        /// <returns>Pessoa</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pessoa>> GetById(int id)
        {
            var pessoa = await _pessoaHandle.GetById(id);
            if (pessoa is null) return NoContent();

            return Ok(pessoa);
        }

        /// <summary>
        /// Cadastra uma nova pessoa
        /// </summary>
        /// <param name="pessoaDTO">Dados da pessoa</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(PessoaDTO pessoaDTO)
        {
            try
            {
                await _pessoaHandle.Create(GetPessoa(pessoaDTO));
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
        /// Atualiza uma pessoa
        /// </summary>
        /// <param name="id">Id da pessoa</param>
        /// <param name="pessoaDTO">Dados da pessoa</param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, PessoaDTO pessoaDTO)
        {
            try
            {
                await _pessoaHandle.Update(id, GetPessoa(pessoaDTO));
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
        /// Exclui uma pessoa
        /// </summary>
        /// <param name="id">Id da pessoa</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _pessoaHandle.Delete(id);
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
        /// Retorna um objeto Pessoa a partir de um PessoaDTO
        /// </summary>
        /// <param name="pessoaDTO"></param>
        /// <returns></returns>
        private Pessoa GetPessoa(PessoaDTO pessoaDTO)
        {
            return new Pessoa()
            {
                Nome = pessoaDTO.Nome,
                CPF = pessoaDTO.CPF,
                DataNascimento = pessoaDTO.DataNascimento,
                Email = pessoaDTO.Email,
                Nacionalidade = pessoaDTO.Nacionalidade,
                Naturalidade = pessoaDTO.Naturalidade,
                Sexo = pessoaDTO.Sexo
            };
        }

    }
}
