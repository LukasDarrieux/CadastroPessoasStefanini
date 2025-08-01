using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Domain.Entities
{
    public class Pessoa
    {
        [Key]
        public int IdPessoa { get; set; }

        [MaxLength(100)]
        public required string Nome { get; set; }

        [MaxLength(11)]
        public required string CPF { get; set; }
        
        public required DateTime DataNascimento { get; set; }
        
        public EnumSexo? Sexo { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? Naturalidade { get; set; }

        [MaxLength(50)]
        public string? Nacionalidade { get; set; }

        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        
    }
}
