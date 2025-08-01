using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace CadastroPessoasStefanini.API.JWT
{
    public class TokenService : IDisposable
    {
        private readonly JWTConfiguracao _jwtConfiguracao;

        public TokenService(IOptions<JWTConfiguracao> jwtConfiguracao) 
        {
            _jwtConfiguracao = jwtConfiguracao.Value;
        }

        /// <summary>
        /// Gera o token JWT
        /// </summary>
        /// <param name="idUsuario">Id Usuário</param>
        /// <returns>Token JWT</returns>
        public string GerarToken(int idUsuario)
        {
            var claims = new[]
            {
                new Claim("IdUsuario", idUsuario.ToString()),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguracao.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var limit = DateTime.Now.AddHours(_jwtConfiguracao.TempoExpiracao);

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguracao.Issuer,
                audience: _jwtConfiguracao.Audience,
                claims: claims,
                expires: limit,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void Dispose()
        { 
        }
    }
}
