using CadastroPessoasStefanini.Data.Context;
using CadastroPessoasStefanini.Data.Repositories.Implements;
using CadastroPessoasStefanini.Domain.DTO;
using CadastroPessoasStefanini.Domain.Entities;
using CadastroPessoasStefanini.Util.Exceptions;
using CadastroPessoasStefanini.Util.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Application
{
    public class UsuarioHandler
    {
        private readonly UsuarioRepository _repository;

        public UsuarioHandler(AppDbContext context) 
        {
            _repository = new UsuarioRepository(context);
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Usuario>? GetById(int idUsuario)
        {
            return await _repository.GetById(idUsuario);
        }

        public async Task<Usuario> Login(LoginDTO login)
        {
            return await _repository.Login(login);
        }

        public async Task Create(Usuario usuario)
        {
            await ValideUsuario(usuario);
            await _repository.Add(usuario);
        }

        public async Task Update(int idUsuario, UsuarioAtualizacaoDTO usuario)
        {
            var usuarioDb = await GetById(idUsuario);
            if (usuarioDb == null)
                throw new ExceptionNotFound();

            usuarioDb.Nome = usuario.Nome;
            
            await _repository.Edit(usuarioDb);
        }

        public async Task Delete(int idUsuario)
        {
            if (await GetById(idUsuario) == null)
                throw new ExceptionNotFound();

            await _repository.Delete(idUsuario);
        }

        /// <summary>
        /// Valida os dados do usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        /// <exception cref="ExceptionValidation"></exception>
        public async Task ValideUsuario(Usuario usuario)
        {
            if (!Validador.EmailValido(usuario.Email))
                throw new ExceptionValidation("E-mail informado é inválido");

            if (await _repository.EmailJaCadastrado(usuario.Email))
                throw new ExceptionValidation("E-mail informado já foi cadastrado");
                
        }
    }
}
