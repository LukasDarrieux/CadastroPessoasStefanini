using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Util.Exceptions
{
    public class ExceptionValidation : Exception
    {
        public IEnumerable<string> Validacoes { get; private set; }

        public ExceptionValidation(IEnumerable<string> validacoes) : base(string.Empty)
        {
            Validacoes = validacoes;
        }

        public ExceptionValidation(string validacao) : this(new List<string> { validacao })
        {
        }
    }
}
