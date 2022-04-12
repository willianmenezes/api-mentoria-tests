using Agendamento.Data.Repositorios.Interfaces;
using Agendamento.Models;
using Agendamento.Services;
using Agendamento.Services.Dtos.Request;
using Moq;
using Moq.AutoMock;
using System;
using Xunit;

namespace AgendamentoTests.ServiceTests
{
    public class ServicoSalaTests
    {
        [Trait("Agendamento", "Servico Sala")]
        [Fact(DisplayName = "Erro ao tentar inserir sala com o mesmo nome")]
        public void Adicionar_ExisteSalaComMesmoNome_DeveRetornarExeption()
        {
            // arranje
            var autoMocker = new AutoMocker();
            var servicoSala = autoMocker.CreateInstance<ServicoSala>();

            var salaRequest = new SalaRequest()
            {
                Andar = 1,
                Nome = "sala teste",
                QuantidadeDeLugares = 10
            };

            autoMocker.GetMock<ISalaRepositorio>().Setup(x => x.BuscarPorNome(salaRequest.Nome)).Returns(new Sala("nome sala", 1, 1));

            // act && assert
            Assert.Throws<Exception>(() => servicoSala.Adicionar(salaRequest));
            autoMocker.GetMock<ISalaRepositorio>().Verify(x => x.Adicionar(It.IsAny<Sala>()), Times.Never);
        }

        [Trait("Agendamento", "Servico Sala")]
        [Fact(DisplayName = "Erro ao alterar nome")]
        public void Adicionar_NovaSala_DeveAdicionarComSucesso()
        {
            // arranje
            var autoMocker = new AutoMocker();
            var servicoSala = autoMocker.CreateInstance<ServicoSala>();

            var salaRequest = new SalaRequest()
            {
                Andar = 1,
                Nome = "sala teste",
                QuantidadeDeLugares = 10
            };

            autoMocker.GetMock<ISalaRepositorio>().Setup(x => x.BuscarPorNome(salaRequest.Nome)).Returns<Sala>(null);

            // act
            servicoSala.Adicionar(salaRequest);

            // assert
            autoMocker.GetMock<ISalaRepositorio>().Verify(x => x.Adicionar(It.IsAny<Sala>()), Times.Once);
        }
    }
}
