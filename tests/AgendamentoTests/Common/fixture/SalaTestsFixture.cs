using Agendamento.Models;
using Agendamento.Services.Dtos.Request;
using Bogus;
using Bogus.DataSets;
using System.Collections.Generic;
using Xunit;

namespace AgendamentoTests.Common.fixture
{
    public class SalaTestsFixture
    {
        public Sala GerarSalaValida()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            return new Faker<Sala>("pt_BR")
                .CustomInstantiator(f => new Sala(
                    f.Name.FindName(gender: genero),
                    f.Random.Int(1, 30),
                    f.Random.Int(1, 30)
                    ));
        }

        public AtualizarSalaRequest GerarAtualizarSalaRequestValida()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            return new Faker<AtualizarSalaRequest>("pt_BR")
                .CustomInstantiator(f => new AtualizarSalaRequest
                {
                    Nome = f.Name.FindName(gender: genero),
                    QuantidadeDeLugares = f.Random.Int(1, 45)
                });
        }

        public List<Sala> GerarSalas(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Sala>("pt_BR")
                .CustomInstantiator(f => new Sala(
                    f.Name.FindName(gender: genero),
                    f.Random.Int(1, 30),
                    f.Random.Int(1, 30)
                    ));

            return cliente.Generate(quantidade);
        }
    }

    [CollectionDefinition(nameof(SalaTestsCollection))]
    public class SalaTestsCollection : ICollectionFixture<SalaTestsFixture> { }
}
