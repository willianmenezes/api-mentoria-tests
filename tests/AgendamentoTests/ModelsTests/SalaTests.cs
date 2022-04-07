using Agendamento.Models;
using AgendamentoTests.ModelsTests.fixture;
using System;
using System.Collections.Generic;
using Xunit;

namespace AgendamentoTests.ModelsTests
{
    [Collection(nameof(SalaTestsCollection))]
    public class SalaTests
    {
        private readonly SalaTestsFixture _salaFixture;

        public SalaTests(SalaTestsFixture salaFixture)
        {
            _salaFixture = salaFixture;
        }

        [Trait("Agendamento", "Sala")]
        [Theory(DisplayName = "Erro ao alterar nome")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("        ")]
        public void AlterarNome_NomeInvalido_DeveRetornarException(string input)
        {
            // arranje 
            var sala = _salaFixture.GerarSalaValida();

            // act && assert
            Assert.Throws<Exception>(() => sala.AlterarNome(input));
        }

        [Trait("Agendamento", "Sala")]
        [Fact(DisplayName = "Alterar nome com sucesso")]
        public void AlterarNome_NomeValido_DeveAlterarONomeComSucesso()
        {
            // Arranje 
            var sala = _salaFixture.GerarSalaValida();
            var novoNome = "Sala Alterada";

            // Act
            sala.AlterarNome(novoNome);

            // Assert
            Assert.Equal(sala.Nome, novoNome);
        }

        [Trait("Agendamento", "Sala")]
        [Theory(DisplayName = "Erro ao alterar quantidade de lugares")]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void AlterarQuantidadeDeLugares_ValorInvalido_DeveRetornarException(int input)
        {
            // arranje 
            var sala = _salaFixture.GerarSalaValida();

            // act && assert
            Assert.Throws<Exception>(() => sala.AlterarQuantidadeDeLugares(input));
        }

        [Trait("Agendamento", "Sala")]
        [Theory(DisplayName = "Alterar quantidade de lugares com sucesso")]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(25)]
        public void AlterarQuantidadeDeLugares_ValorValido_DeveAlterarQuantidadeComSucesso(int novaQuantidadeDeLugares)
        {
            // arranje 
            var sala = _salaFixture.GerarSalaValida();

            // act
            sala.AlterarQuantidadeDeLugares(novaQuantidadeDeLugares);

            // Assert
            Assert.Equal(sala.QuantidadeDeLugares, novaQuantidadeDeLugares);
        }
    }
}
