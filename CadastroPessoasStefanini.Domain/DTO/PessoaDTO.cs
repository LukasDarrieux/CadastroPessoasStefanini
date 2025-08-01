using CadastroPessoasStefanini.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Domain.DTO
{
    public class PessoaDTO
    {
        public int IdPessoa { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required DateTime DataNascimento { get; set; }
        public EnumSexo? Sexo { get; set; }
        public string? Email { get; set; }
        public string? Naturalidade { get; set; }
        public string? Nacionalidade { get; set; }
    }
}
