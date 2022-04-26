using Agendamento.Data.Repositorios.Interfaces;
using Agendamento.Models;
using Agendamento.Services;
using Agendamento.Services.Dtos.Request;
using AgendamentoTests.Common.fixture;
using Moq;
using Moq.AutoMock;
using System;
using System.Linq;
using Xunit;

namespace AgendamentoTests.ServiceTests
{
    [Collection(nameof(SalaTestsCollection))]
    public class ServicoSalaTests
    {
        private readonly SalaTestsFixture _salaFixture;

        public ServicoSalaTests(SalaTestsFixture salaFixture)
        {
            _salaFixture = salaFixture;
        }

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
        [Fact(DisplayName = "Adicionar sala com sucesso")]
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


        [Trait("Agendamento", "Servico Sala")]
        [Fact(DisplayName = "Buscar salas existentes")]
        public void Buscar_SalasExistentes_DeveRetornarTodasAsSalas()
        {
            // arranje
            var autoMocker = new AutoMocker();
            var servicoSala = autoMocker.CreateInstance<ServicoSala>();

            autoMocker.GetMock<ISalaRepositorio>().Setup(x => x.BuscarTodos()).Returns(_salaFixture.GerarSalas(5));

            // act
            var salas = servicoSala.Buscar();

            // assert
            Assert.True(salas.Count > 0);
            autoMocker.GetMock<ISalaRepositorio>().Verify(x => x.BuscarTodos(), Times.Once);
        }

        [Trait("Agendamento", "Servico Sala")]
        [Fact(DisplayName = "Buscar salas inexistentes")]
        public void Buscar_SalasInexistentes_DeveRetornarListaVazia()
        {
            // arranje
            var autoMocker = new AutoMocker();
            var servicoSala = autoMocker.CreateInstance<ServicoSala>();

            autoMocker.GetMock<ISalaRepositorio>().Setup(x => x.BuscarTodos()).Returns(Enumerable.Empty<Sala>().ToList());

            // act
            var salas = servicoSala.Buscar();

            // assert
            Assert.True(salas?.Count == 0);
            autoMocker.GetMock<ISalaRepositorio>().Verify(x => x.BuscarTodos(), Times.Once);
        }

        [Trait("Agendamento", "Servico Sala")]
        [Fact(DisplayName = "Erro ao tentar remover uma sala inexistente")]
        public void Remover_SalaInexistente_DeveRetornarExeption()
        {
            // arranje
            var autoMocker = new AutoMocker();
            var servicoSala = autoMocker.CreateInstance<ServicoSala>();
            var guid = Guid.NewGuid();

            autoMocker.GetMock<ISalaRepositorio>().Setup(x => x.BuscarPorId(guid)).Returns<Sala>(null);

            // act && assert
            Assert.Throws<Exception>(() => servicoSala.Remover(guid));
            autoMocker.GetMock<ISalaRepositorio>().Verify(x => x.Remover(It.IsAny<Sala>()), Times.Never);
        }

        [Trait("Agendamento", "Servico Sala")]
        [Fact(DisplayName = "Remover sala existente")]
        public void Remover_SalaExistente_DeveRemoverComSucesso()
        {
            // arranje
            var autoMocker = new AutoMocker();
            var servicoSala = autoMocker.CreateInstance<ServicoSala>();
            var guid = Guid.NewGuid();
            var sala = _salaFixture.GerarSalaValida();

            autoMocker.GetMock<ISalaRepositorio>().Setup(x => x.BuscarPorId(guid)).Returns(_salaFixture.GerarSalaValida());

            // act
            servicoSala.Remover(guid);

            // assert
            autoMocker.GetMock<ISalaRepositorio>().Verify(x => x.Remover(It.IsAny<Sala>()), Times.Once);
        }

        [Trait("Agendamento", "Servico Sala")]
        [Fact(DisplayName = "Erro ao tentar editar uma sala inexistente")]
        public void Editar_SalaInexistente_DeveRetornarExeption()
        {
            // arranje
            var autoMocker = new AutoMocker();
            var servicoSala = autoMocker.CreateInstance<ServicoSala>();
            var guid = Guid.NewGuid();
            var salaRequest = _salaFixture.GerarAtualizarSalaRequestValida();

            autoMocker.GetMock<ISalaRepositorio>().Setup(x => x.BuscarPorId(guid)).Returns<Sala>(null);

            // act && assert
            Assert.Throws<Exception>(() => servicoSala.Editar(guid, salaRequest));
            autoMocker.GetMock<ISalaRepositorio>().Verify(x => x.Atualizar(It.IsAny<Sala>()), Times.Never);
        }

        [Trait("Agendamento", "Servico Sala")]
        [Fact(DisplayName = "Editar sala existente")]
        public void Editar_SalaExistente_DeveEditarComSucesso()
        {
            // arranje
            var autoMocker = new AutoMocker();
            var servicoSala = autoMocker.CreateInstance<ServicoSala>();
            var guid = Guid.NewGuid();
            var salaRequest = _salaFixture.GerarAtualizarSalaRequestValida();

            autoMocker.GetMock<ISalaRepositorio>().Setup(x => x.BuscarPorId(guid)).Returns(_salaFixture.GerarSalaValida());

            // act
            servicoSala.Editar(guid, salaRequest);

            // assert
            autoMocker.GetMock<ISalaRepositorio>().Verify(x => x.Atualizar(It.IsAny<Sala>()), Times.Once);
        }

    }
}
