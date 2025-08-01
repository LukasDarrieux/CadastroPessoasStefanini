using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Util.Exceptions
{
    public class ExceptionNotFound : Exception
    {
        public ExceptionNotFound() : base("Registro não encontrado") 
        {
        }
    }
}
