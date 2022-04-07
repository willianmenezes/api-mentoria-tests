using Agendamento.Models;
using Bogus;
using Bogus.DataSets;
using System.Collections.Generic;
using Xunit;

namespace AgendamentoTests.ModelsTests.fixture
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

        public List<Sala> GerarSalas(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente =  new Faker<Sala>("pt_BR")
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
