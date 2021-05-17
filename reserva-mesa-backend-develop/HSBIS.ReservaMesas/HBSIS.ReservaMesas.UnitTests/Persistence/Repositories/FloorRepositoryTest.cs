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
    public class FloorRepositoryTest
    {
        private readonly MainContext _dbContext;
        private readonly IFloorRepository _floorRepository;

        public FloorRepositoryTest()
        {
            _dbContext = new MainContextHelper().CreateInMemoryMainContext();
            new InitialFloorSeed().SeedData(_dbContext);
            _floorRepository = new FloorRepository(_dbContext);
        }

        [Fact]
        public async Task Should_Get_All_Floors()
        {
            var floors = await _floorRepository.GetAll();

            floors.Should().HaveCount(10);
            floors.Should().Contain(floor => floor.Name == "Andar 1");
            floors.Should().Contain(floor => floor.Name == "Andar 2");
            floors.Should().Contain(floor => floor.Name == "Andar 3");
            floors.Should().Contain(floor => floor.Name == "Andar 4");
            floors.Should().Contain(floor => floor.Name == "Andar 5");
            floors.Should().Contain(floor => floor.Name == "Andar 6");
            floors.Should().Contain(floor => floor.Name == "Andar 7");
            floors.Should().Contain(floor => floor.Name == "Andar 8");
            floors.Should().Contain(floor => floor.Name == "Andar 9");
            floors.Should().Contain(floor => floor.Name == "Andar 10");
        }

        [Fact]
        public async Task Should_Get_All_Floors_By_UnitsId()
        {
            var unityId = 1;

            var floors = await _floorRepository.GetFloorsByUnityId(unityId);

            floors.Should().HaveCount(10);
            floors.Should().Contain(floor => floor.Name == "Andar 1");
            floors.Should().Contain(floor => floor.Name == "Andar 2");
            floors.Should().Contain(floor => floor.Name == "Andar 3");
            floors.Should().Contain(floor => floor.Name == "Andar 4");
            floors.Should().Contain(floor => floor.Name == "Andar 5");
            floors.Should().Contain(floor => floor.Name == "Andar 6");
            floors.Should().Contain(floor => floor.Name == "Andar 7");
            floors.Should().Contain(floor => floor.Name == "Andar 8");
            floors.Should().Contain(floor => floor.Name == "Andar 9");
            floors.Should().Contain(floor => floor.Name == "Andar 10");
        }
    }
}
