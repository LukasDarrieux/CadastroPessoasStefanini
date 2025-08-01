using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.API
{
    public class JWTConfiguracao
    {
        /// <summary>
        /// Chave secreta do token
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Emissor do Token
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Destino do Token
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Tempo de expiração do token
        /// </summary>
        public int TempoExpiracao { get; set; }
    }
}
