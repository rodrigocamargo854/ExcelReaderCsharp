using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Workstations;
using HBSIS.ReservaMesas.Application.Services;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using NSubstitute;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.Services
{
    public class WorkstationServiceTest
    {
        private readonly IWorkstationService _workstationService;
        private readonly IWorkstationRepository _workstationRepository;

        public WorkstationServiceTest()
        {
            _workstationRepository = Substitute.For<IWorkstationRepository>();
            _workstationService = new WorkstationService(_workstationRepository);
        }

        [Fact]
        public async Task Should_Get_Workstation_By_Name()
        {
            var workstation = new Workstation("01-02-01-01", false, 1);

            var workstationName = "01-02-01-01";

            var workstationId = 1;

            workstation.Id = workstationId;

            _workstationRepository.GetByName(workstationName)
                            .Returns(workstation);

            var workstationIdReturned = await _workstationService.GetByName(workstationName);

            workstationIdReturned.Should().NotBeNull();
            workstationIdReturned.Id.Should().Be(workstationId);
            workstationIdReturned.Name.Should().Be(workstation.Name);
            workstationIdReturned.Active.Should().BeFalse();
            workstationIdReturned.FloorId.Should().Be(1);

            await _workstationRepository.Received(1).GetByName(workstationName);
        }

        [Fact]
        public async Task Should_Not_Found_Workstation_When_Name_Not_Exists()
        {
            var workstationName = "01-02-01-01";

            Workstation workstation = null;

            _workstationRepository.GetByName(workstationName)
                            .Returns(workstation);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await _workstationService.GetByName(workstationName));
            ex.Message.Should().Contain("Estação de trabalho não encontrada!");
        }

        [Fact]
        public async Task Should_Update_Workstation()
        {
            var workstationModel = new WorkstationRequestModel()
            {
                Active = true,
            };

            var workstation = new Workstation("01-02-01-01", true, 1);

            var workstationName = "01-02-01-01";

            var workstationId = 1;

            workstation.Id = workstationId;

            _workstationRepository.GetByName(workstationName)
                            .Returns(workstation);

            await _workstationService.Update(workstationName, workstationModel);

            _workstationRepository.Received(1).Update(Arg.Is<Workstation>(x => x.Active));
        }

        [Fact]
        public async Task Should_Not_Update_Workstation_When_Is_Null()
        {
            var workstationName = "01-02-01-01";

            Workstation workstation = null;

            var workstationModel = new WorkstationRequestModel()
            {
                Active = true,
            };

            _workstationRepository.GetByName(workstationName)
                            .Returns(workstation);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await _workstationService.Update(workstationName, workstationModel));
            ex.Message.Should().Contain("Estação de trabalho não encontrada!");
        }

        [Fact]
        public async Task Should_Get_Inactive_Workstations_By_Floor()
        {
            var workstations = new List<Workstation>();
            workstations.Add(new Workstation("01-02-01-02", false, 2));
            _workstationRepository.GetInactivesByFloor(Arg.Any<int>()).Returns(workstations);
            var receivedWorkstations = await _workstationService.GetInactivesByFloor(2);
            receivedWorkstations.Should().NotBeNull();
            receivedWorkstations.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_Not_Get_Inactives_By_Floor()
        {
            var workstations = new List<Workstation>();

            _workstationRepository.GetInactivesByFloor(Arg.Any<int>()).Returns(workstations);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await _workstationService.GetInactivesByFloor(1));
            ex.Message.Should().Contain("Estações de trabalho não encontradas.");
        }
    }
}
