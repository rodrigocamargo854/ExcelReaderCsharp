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
    public class WorkstationRepositoryTest
    {
        private readonly MainContext _dbContext;
        private readonly IWorkstationRepository _workstationRepository;

        public WorkstationRepositoryTest()
        {
            _dbContext = new MainContextHelper().CreateInMemoryMainContext();
            new InitialWorkstationSeed().SeedData(_dbContext);
            _workstationRepository = new WorkstationRepository(_dbContext);
        }

        [Fact]
        public async Task Get_Workstation_By_Name()
        {
            var workstationName = "01-02-01-01";

            var workstation = await _workstationRepository.GetByName(workstationName);

            workstation.Should().NotBeNull();
            workstation.Name.Should().Be("01-02-01-01");
            workstation.FloorId.Should().Be(2);
            workstation.Active.Should().BeFalse();
        }

        [Fact]
        public async Task Get_Inactive_Workstations_By_Floor()
        {
            var workstations = await _workstationRepository.GetInactivesByFloor(2);

            workstations.Should().NotBeNull();
            workstations.Should().NotBeEmpty();
        }
    }
}
