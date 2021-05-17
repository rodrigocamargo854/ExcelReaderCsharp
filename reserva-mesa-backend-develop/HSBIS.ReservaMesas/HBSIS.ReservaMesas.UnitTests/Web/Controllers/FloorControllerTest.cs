using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Floors;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Web.Controllers
{
    public class FloorControllerTest
    {
        private readonly IFloorService _floorService;
        private readonly FloorController _controller;

        public FloorControllerTest()
        {
            _floorService = Substitute.For<IFloorService>();
            _controller = new FloorController(_floorService);
        }

        [Fact]
        public async Task If_Update_WithValidData_Return_Ok()
        {
            var floor = new FloorRequestModel()
            {
                Name = "test",
                Active = true,
                UnityId = 1
            };

            var data = await _controller.Put(1, floor);

            await _floorService.Received(1).Update(1, floor);

            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async Task If_Update_WithInvalidData_Return_NotFound()
        {
            _floorService
                .When(x => x.Update(Arg.Any<int>(), Arg.Any<FloorRequestModel>()))
                .Throw(new NotFoundException());

            var data = await _controller.Put(1, null);

            await _floorService.Received(1).Update(1, null);

            Assert.IsType<NotFoundObjectResult>(data);
        }

        [Fact]
        public async Task If_Update_WithInvalidData_Return_StatusCode()
        {
            _floorService
               .When(x => x.Update(Arg.Any<int>(), Arg.Any<FloorRequestModel>()))
               .Throw(new Exception());

            var data = await _controller.Put(1, null);

            await _floorService.Received(1).Update(1, null);

            Assert.IsType<StatusCodeResult>(data);
            Assert.Equal(500, ((StatusCodeResult)data).StatusCode);
        }

        [Fact]
        public async Task If_Update_WithInvalidData_Return_BadRequest()
        {
            _floorService
               .When(x => x.Update(Arg.Any<int>(), Arg.Any<FloorRequestModel>()))
               .Throw(new CustomValidationException());

            var data = await _controller.Put(1, null);

            await _floorService.Received(1).Update(1, null);

            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task If_GetById_WithValidId_Return_OkResult()
        {
            var floorModel = new FloorResponseModel(1, "Andar 3", true, "01-03", 1);

            _floorService.GetById(1).Returns(floorModel);

            var response = await _controller.GetById(1);

            await _floorService.Received(1).GetById(1);

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task If_GetById_WithInvalidId_Return_NotFound()
        {
            _floorService
                 .When(x => x.GetById(100))
                 .Do(x => { throw new NotFoundException(); });

            var data = await _controller.GetById(100);

            await _floorService.Received(1).GetById(100);

            Assert.IsType<NotFoundObjectResult>(data);
        }

        [Fact]
        public async Task If_GetById_WithInvalidData_Return_StatusCode()
        {
            _floorService
               .When(x => x.GetById(Arg.Any<int>()))
               .Throw(new Exception());

            var data = await _controller.GetById(1);

            await _floorService.Received(1).GetById(1);

            Assert.IsType<StatusCodeResult>(data);
        }

        [Fact]
        public async Task If_GetFloorsByUnityId_Return_Ok()
        {
            var floors = new List<FloorResponseModel>();
            floors.Add(new FloorResponseModel(1, "Andar 3", true, "01-03", 1));

            _floorService.GetFloorsByUnityId(1).Returns(floors);

            var response = await _controller.GetFloorsByUnityId(1);

            await _floorService.Received(1).GetFloorsByUnityId(1);

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task If_GetFloorsByUnityId_WithInvalidId_Return_NotFound()
        {
            _floorService
                 .When(x => x.GetFloorsByUnityId(100))
                 .Do(x => { throw new NotFoundException(); });

            var data = await _controller.GetFloorsByUnityId(100);

            await _floorService.Received(1).GetFloorsByUnityId(100);

            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task If_GetFloorsByUnityId_WithInvalidData_Return_StatusCode()
        {
            _floorService
               .When(x => x.GetFloorsByUnityId(Arg.Any<int>()))
               .Throw(new Exception());

            var data = await _controller.GetFloorsByUnityId(1);

            await _floorService.Received(1).GetFloorsByUnityId(1);

            Assert.IsType<StatusCodeResult>(data);
        }

        [Fact]
        public async Task Should_Return_Ok_StatusCode_On_Get_By_Code()
        {
            var responseModel = new FloorResponseModel(1, "test", true, "01-03", 1);
            _floorService.GetByCode(Arg.Any<string>()).Returns(responseModel);

            var response = await _controller.GetByCode("test");

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Should_Return_Ok_BadRequest_On_Get_By_Code_And_Throw_Not_Found_Exception()
        {
            var responseModel = new FloorResponseModel(1, "test", true, "01-03", 1);
            _floorService.When(x => x.GetByCode(Arg.Any<string>())).Throw(new NotFoundException());

            var response = await _controller.GetByCode("test");

            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task Should_Return_Ok_500StatusCode_On_Get_By_Code_And_Throw_Exception()
        {
            var responseModel = new FloorResponseModel(1, "test", true, "01-03", 1);
            _floorService.When(x => x.GetByCode(Arg.Any<string>())).Throw(new Exception());

            var response = await _controller.GetByCode("test");

            Assert.Equal(500, ((StatusCodeResult)response).StatusCode);
        }
    }
}
