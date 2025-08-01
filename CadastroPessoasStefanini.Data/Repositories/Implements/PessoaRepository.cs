using CadastroPessoasStefanini.Data.Context;
using CadastroPessoasStefanini.Data.Repositories.Interfaces;
using CadastroPessoasStefanini.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Data.Repositories.Implements
{
    public class PessoaRepository : Repository<Pessoa>, IDisposable
    {
        public PessoaRepository(AppDbContext context): base(context, context.Pessoas)
        {
        }

        /// <summary>
        /// Verifica se o CPF informado já esta cadastrado na base de dados
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public Pessoa? GetByCPF(string cpf)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.CPF == cpf);
            return pessoa;
        }


        public void Dispose() 
        { 
        }
    }
}
