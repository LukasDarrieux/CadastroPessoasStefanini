using CadastroPessoasStefanini.Util.Validacoes;

namespace CadastroPessoasStefanini.Util.Test
{
    public class ValidadorUnitTest
    {
        #region Testes validação CPF

        [Fact]
        public void TesteRemoveMascaraCPF()
        {
            string cpf = "123.456.789-09";
            string cpfSemMascara = "12345678909";
            var resultado = Validador.RemoveMascara(cpf);

            Assert.Equal(resultado, cpfSemMascara);
        }

        [Fact]
        public void TesteCPFValido()
        {
            string cpf = "265.574.660-03";
            var resultado = Validador.ValideCPF(cpf);

            Assert.True(resultado);
        }

        [Fact]
        public void TesteCPFInvalido()
        {
            string cpf = "123.456.789-12";
            var resultado = Validador.ValideCPF(cpf);

            Assert.False(resultado);
        }

        #endregion

        #region Testes validação e-mail

        [Fact]
        public void TesteEmailValido()
        {
            string email = "meu_email_valido@teste.com";
            var resultado = Validador.EmailValido(email);

            Assert.True(resultado);
        }

        [Fact]
        public void TesteEmailValidoComNacionalidade()
        {
            string email = "meu_email_valido@teste.com.br";
            var resultado = Validador.EmailValido(email);

            Assert.True(resultado);
        }

        [Fact]
        public void TesteEmailInvalido()
        {
            string email = "meu_email_invalido@teste";
            var resultado = Validador.EmailValido(email);

            Assert.False(resultado);
        }

        #endregion
    }
}