using FluentAssertions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using HBSIS.ReservaMesas.Persistence.Context;
using HBSIS.ReservaMesas.Persistence.Repositories;
using HBSIS.ReservaMesas.Persistence.Seeds;
using HBSIS.ReservaMesas.UnitTests.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Persistence.Repositories
{
    [Collection("Non-Parallel Collection")]
    public class UnityRepositoryTest
    {
        private readonly MainContext _dbContext;
        private readonly IUnityRepository _unityRepository;

        public UnityRepositoryTest()
        {
            _dbContext = new MainContextHelper().CreateInMemoryMainContext();
            new InitialUnitySeed().SeedData(_dbContext);
            _unityRepository = new UnityRepository(_dbContext);
        }

        [Fact]
        public async Task Should_Get_All_Units()
        {
            var units = await _unityRepository.GetAll();

            units.Should().HaveCount(3);
            units.Should().Contain(unit => unit.Name == "Maringá");
            units.Should().Contain(unit => unit.Name == "Blumenau");
            units.Should().Contain(unit => unit.Name == "Sorocaba");
        }
    }
}
