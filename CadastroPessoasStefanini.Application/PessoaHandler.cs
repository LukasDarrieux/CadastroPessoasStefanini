using CadastroPessoasStefanini.Data.Context;
using CadastroPessoasStefanini.Data.Repositories.Implements;
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
    public class PessoaHandler
    {
        private readonly PessoaRepository _repository;

        public PessoaHandler(AppDbContext context)
        {
            _repository = new PessoaRepository(context);
        }

        public async Task<IEnumerable<Pessoa>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Pessoa>? GetById(int idPessoa)
        {
            return await _repository.GetById(idPessoa);
        }

        public async Task Create(Pessoa pessoa)
        {
            ValidePessoaInclusao(pessoa);
            pessoa.CPF = Validador.RemoveMascara(pessoa.CPF);
            pessoa.DataCadastro = DateTime.Now;
            await _repository.Add(pessoa);
        }

        public async Task Update(int idPessoa, Pessoa pessoa)
        {
            var pessoaDb = await GetById(idPessoa);
            if (pessoaDb == null)
                throw new ExceptionNotFound();

            ValidePessoaAlteracao(idPessoa, pessoa);

            pessoaDb.CPF = Validador.RemoveMascara(pessoa.CPF);
            pessoaDb.DataNascimento = pessoa.DataNascimento;
            pessoaDb.Email = pessoa.Email;
            pessoaDb.Nacionalidade = pessoa.Nacionalidade;
            pessoaDb.Naturalidade = pessoa.Naturalidade;
            pessoaDb.Nome = pessoa.Nome;
            pessoaDb.Sexo = pessoa.Sexo;
            pessoaDb.DataAtualizacao = DateTime.Now;
            
            await _repository.Edit(pessoaDb);
        }

        public async Task Delete(int idPessoa)
        {
            if (await GetById(idPessoa) == null)
                throw new ExceptionNotFound();
                
            await _repository.Delete(idPessoa);
        }

        /// <summary>
        /// Realiza as validações dos dados da pessoa
        /// </summary>
        /// <param name="pessoa"></param>
        /// <exception cref="ExceptionValidation"></exception>
        private void ValidePessoa(Pessoa pessoa)
        {
            if (pessoa is null)
                throw new ExceptionValidation("Objeto Null");

            if (string.IsNullOrEmpty(pessoa.Nome.Trim()))
                throw new ExceptionValidation("Nome da pessoa não informado");

            if (pessoa.DataNascimento == DateTime.MinValue)
                throw new ExceptionValidation("Data de nascimento não informada");

            if (string.IsNullOrEmpty(pessoa.CPF))
                throw new ExceptionValidation("CPF não informado");

            if (!Validador.ValideCPF(pessoa.CPF))
                throw new ExceptionValidation("CPF informado inválido");

            if (!string.IsNullOrEmpty(pessoa.Email))
            {
                if (!Validador.EmailValido(pessoa.Email))
                    throw new ExceptionValidation("E-mail informado é inválido");
            }
        }
        
        /// <summary>
        /// Valida os dados da pessoa ao incluir
        /// </summary>
        /// <param name="pessoa"></param>
        /// <exception cref="ExceptionValidation"></exception>
        public void ValidePessoaInclusao(Pessoa pessoa)
        {
            ValidePessoa(pessoa);

            var pessoaDb = _repository.GetByCPF(Validador.RemoveMascara(pessoa.CPF));
            if (pessoaDb != null)
                throw new ExceptionValidation("O CPF informado já existe");
        }

        /// <summary>
        /// Valida os dados da pessoa ao atualizar
        /// </summary>
        /// <param name="idPessoa"></param>
        /// <param name="pessoa"></param>
        /// <exception cref="ExceptionValidation"></exception>
        public void ValidePessoaAlteracao(int idPessoa, Pessoa pessoa)
        {
            ValidePessoa(pessoa);

            var pessoaDb = _repository.GetByCPF(Validador.RemoveMascara(pessoa.CPF));
            if (pessoaDb != null)
            {
                if (pessoaDb.IdPessoa != idPessoa)
                    throw new ExceptionValidation("O CPF informado já existe");
            }
        }
    }
}
