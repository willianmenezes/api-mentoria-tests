using Agendamento.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace AgendamentoTests.ModelsTests
{
    public class SalaTest
    {

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("        ")]
        public void AlterarNome_NomeInvalido_DeveRetornarException(string input)
        {
            // arranje 
            var sala = new Sala("Sala Teste", 10, 1);

            // act && assert
            Assert.Throws<Exception>(() => sala.AlterarNome(input));
        }

        [Fact]
        public void AlterarNome_NomeValido_DeveAlterarONomeComSucesso ()
        {
            // arranje 
            var sala = new Sala("Sala Teste", 10, 1);
            var novoNome = "Sala Alterada";

            // act
            sala.AlterarNome(novoNome);

            // Assert
            Assert.Equal(sala.Nome, novoNome);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void AlterarQuantidadeDeLugares_ValorInvalido_DeveRetornarException(int input)
        {
            // arranje 
            var sala = new Sala("Sala Teste", 10, 1);

            // act && assert
            Assert.Throws<Exception>(() => sala.AlterarQuantidadeDeLugares(input));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(25)]
        public void AlterarQuantidadeDeLugares_ValorValido_DeveAlterarQuantidadeComSucesso(int novaQuantidadeDeLugares)
        {
            // arranje 
            var sala = new Sala("Sala Teste", 99, 1);

            // act
            sala.AlterarQuantidadeDeLugares(novaQuantidadeDeLugares);

            // Assert
            Assert.Equal(sala.QuantidadeDeLugares, novaQuantidadeDeLugares);
        }
    }
}
