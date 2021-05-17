using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Floors;
using HBSIS.ReservaMesas.Application.Services;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using NSubstitute;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.Services
{
    public class FloorServiceTest
    {
        private readonly IFloorService _floorService;
        private readonly IFloorRepository _floorRepository;

        public FloorServiceTest()
        {
            _floorRepository = Substitute.For<IFloorRepository>();
            _floorService = new FloorService(_floorRepository);
        }

        [Fact]
        public async Task Should_Update_Floor()
        {
            var floorModel = new FloorRequestModel()
            {
                Name = "Andar 2",
                Active = true,
                UnityId = 1
            };

            var floor = new Floor("Andar 3", true, "01-03", 1);

            var floorId = 1;

            floor.Id = floorId;

            _floorRepository.GetById(floorId)
                            .Returns(floor);

            await _floorService.Update(floorId, floorModel);

            _floorRepository.Received(1).Update(Arg.Is<Floor>(x => x.Name == "Andar 2" && x.UnityId == 1 && x.Active));
        }

        [Fact]
        public async Task Should_Not_Update_Floor_When_Is_Null()
        {
            var floorId = 1;

            Floor floor = null;

            var floorModel = new FloorRequestModel()
            {
                Name = "Andar 2",
                Active = true,
                UnityId = 1
            };

            _floorRepository.GetById(floorId)
                            .Returns(floor);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await _floorService.Update(floorId, floorModel));
            ex.Message.Should().Contain("Andar não encontrado!");
        }

        [Fact]
        public async Task Should_Delete_Floor()
        {
            var floor = new Floor("Andar 1", true, "01-03", 1);

            var floorId = 1;

            floor.Id = floorId;

            _floorRepository.GetById(floorId)
                            .Returns(floor);

            await _floorService.Delete(floorId);

            _floorRepository.Received(1).Update(Arg.Is<Floor>(x => x.Deleted));

            await _floorRepository.Received(1).Save();
        }

        [Fact]
        public async Task Should_Not_Delete_Floor_When_Is_Null()
        {
            var floorId = 0;

            await _floorRepository.GetById(floorId);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await _floorService.Delete(floorId));
            ex.Message.Should().Contain("Andar não encontrado!");
        }

        [Fact]
        public async Task Should_Get_Floor_By_Id()
        {
            var floor = new Floor("Andar 3", true, "01-03", 1);

            var floorId = 1;

            floor.Id = floorId;

            _floorRepository.GetById(floorId)
                            .Returns(floor);

            var floorReturned = await _floorService.GetById(floorId);

            floorReturned.Should().NotBeNull();
            floorReturned.Id.Should().Be(floorId);
            floorReturned.Name.Should().Be(floor.Name);
            floorReturned.Active.Should().BeTrue();
            floorReturned.UnityId.Should().Be(1);

            await _floorRepository.Received(1).GetById(floorId);
        }

        [Fact]
        public async Task Should_Not_Found_Floor_When_Id_Not_Exists()
        {
            int floorId = 1;

            Floor floor = null;

            _floorRepository.GetById(floorId)
                            .Returns(floor);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await _floorService.GetById(floorId));
            ex.Message.Should().Contain("Andar não encontrado!");
        }

        [Fact]
        public async Task Should_Get_Alls_Floors()
        {
            var floorIdA = 1;
            var floorIdB = 2;

            var floorA = new Floor("Andar 3", true, "01-03", 1);
            var floorB = new Floor("Andar 3", true, "01-03", 1);

            floorA.Id = floorIdA;
            floorB.Id = floorIdB;

            var insertedFloors = new List<Floor>()
            {
                floorA,
                floorB
            };

            _floorRepository.GetAll()
                            .Returns(insertedFloors);

            var floorReturned = await _floorService.GetAll();

            floorReturned.Should().HaveCount(2);

            floorReturned.Any(x => x.Name == floorA.Name &&
                                   x.UnityId == floorA.UnityId &&
                                   x.Id == floorA.Id &&
                                   x.Active).Should().BeTrue();

            floorReturned.Any(x => x.Name == floorB.Name &&
                                   x.UnityId == floorB.UnityId &&
                                   x.Id == floorB.Id &&
                                   x.Active).Should().BeTrue();

            await _floorRepository.Received(1).GetAll();
        }

        [Fact]
        public async Task Should_Not_Found_Floors_When_Id_Not_Exists()
        {
            List<Floor> insertedFloors = null;

            _floorRepository.GetAll()
                            .Returns(insertedFloors);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await _floorService.GetAll());
            ex.Message.Should().Contain("Andar não encontrado!");
        }

        [Fact]
        public async Task Should_Get_Floors_By_Unity_Id()
        {
            var unityId = 1;

            var floorA = new Floor("Andar 3", true, "01-03", unityId); 
            var floorB = new Floor("Andar 2", true, "01-02", unityId);

            var insertedFloors = new List<Floor>()
            {
                floorA,
                floorB
            };

            _floorRepository.GetFloorsByUnityId(unityId)
                            .Returns(insertedFloors);

            var floorsReturned = await _floorService.GetFloorsByUnityId(unityId);

            floorsReturned.Should().NotBeNull();

            floorsReturned.Should().HaveCount(2);

            floorsReturned.Any(x => x.Name == floorA.Name &&
                                   x.UnityId == unityId &&
                                   x.Id == floorA.Id &&
                                   x.Active).Should().BeTrue();

            floorsReturned.Any(x => x.Name == floorB.Name &&
                                   x.UnityId == unityId &&
                                   x.Id == floorB.Id &&
                                   x.Active).Should().BeTrue();

            await _floorRepository.Received(1).GetFloorsByUnityId(unityId);
        }

        [Fact]
        public async Task Should_Not_Get_Floors_By_Unity_Id()
        {
            var unityId = 1;

            var insertedFloors = new List<Floor>();

            _floorRepository.GetFloorsByUnityId(unityId)
                            .Returns(insertedFloors);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () =>
                      await _floorService.GetFloorsByUnityId(unityId));

            ex.Message.Should().Contain("Andares não encontrados!");
        }

        [Fact]
        public async Task Should_Get_Floor_By_Code()
        {
            Floor registeredFloor = new Floor("teste", true, "teste", 1);
            _floorRepository.GetByCode(Arg.Any<string>()).Returns(registeredFloor);

            var floor = await _floorService.GetByCode("teste");

            floor.Name.Should().Be(registeredFloor.Name);
            floor.Code.Should().Be(registeredFloor.Code);
            floor.Active.Should().Be(registeredFloor.Active);
            floor.UnityId.Should().Be(registeredFloor.UnityId);
            await _floorRepository.Received(1).GetByCode(Arg.Any<string>());
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_On_Get_Floor_By_Code()
        {
            _floorRepository.GetByCode(Arg.Any<string>()).Returns(null as Floor);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await _floorService.GetByCode("teste"));

            ex.Should().NotBeNull();
            await _floorRepository.Received(1).GetByCode(Arg.Any<string>());
        }
    }
}
