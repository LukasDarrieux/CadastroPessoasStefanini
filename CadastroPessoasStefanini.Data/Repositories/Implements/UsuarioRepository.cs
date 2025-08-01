using CadastroPessoasStefanini.Data.Context;
using CadastroPessoasStefanini.Domain.DTO;
using CadastroPessoasStefanini.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Data.Repositories.Implements
{
    public class UsuarioRepository : Repository<Usuario>, IDisposable
    {
        public UsuarioRepository(AppDbContext context): base(context, context.Usuarios)
        {
        }

        /// <summary>
        /// Verifica se o e-mail e senha estão cadastrados para efetuar o login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<Usuario>? Login(LoginDTO login)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == login.Email);
            if (usuario is null || usuario.Senha != login.Senha) return null;
            return usuario;
        }

        /// <summary>
        /// Verifica se o e-mail informado já esta cadastrado
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>true | false</returns>
        public async Task<bool> EmailJaCadastrado(string email)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            return (usuario != null);
        }

        public void Dispose()
        {
        }
    }
}
