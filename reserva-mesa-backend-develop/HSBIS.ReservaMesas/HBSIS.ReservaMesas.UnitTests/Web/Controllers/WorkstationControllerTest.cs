using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Workstations;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Web.Controllers
{
    public class WorkstationControllerTest
    {
        private readonly IWorkstationService _workstationService;
        private readonly WorkstationController _controller;

        public WorkstationControllerTest()
        {
            _workstationService = Substitute.For<IWorkstationService>();
            _controller = new WorkstationController(_workstationService);
        }

        [Fact]
        public async Task If_GetByName_WithValidId_Return_OkResult()
        {
            var workstationModel = new WorkstationResponseModel(1, "01-02-01-01", false, 2);

            _workstationService.GetByName("01-02-01-01")
                               .Returns(workstationModel);

            var response = await _controller.GetByName("01-02-01-01");

            await _workstationService.Received(1).GetByName("01-02-01-01");

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task If_GetByName_WithInvalidId_Return_NotFound()
        {
            _workstationService
                 .When(x => x.GetByName("01-02-01-01"))
                 .Do(x => { throw new NotFoundException(); });

            var data = await _controller.GetByName("01-02-01-01");

            await _workstationService.Received(1).GetByName("01-02-01-01");

            Assert.IsType<NotFoundObjectResult>(data);
        }

        [Fact]
        public async Task If_GetByName_WithInvalidData_Return_StatusCode()
        {
            _workstationService
               .When(x => x.GetByName(Arg.Any<string>()))
               .Throw(new Exception());

            var data = await _controller.GetByName("01-02-01-01");

            await _workstationService.Received(1).GetByName("01-02-01-01");

            Assert.IsType<StatusCodeResult>(data);
        }

        [Fact]
        public async Task If_Update_WithValidData_Return_Ok()
        {
            var workstation = new WorkstationRequestModel()
            {
                Name = "01-02-01-01",
                Active = true
            };

            var data = await _controller.Put(workstation);

            await _workstationService.Received(1).Update("01-02-01-01", workstation);

            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async Task If_Update_WithInvalidData_Return_NotFound()
        {
            _workstationService
                .When(x => x.Update(Arg.Any<string>(), Arg.Any<WorkstationRequestModel>()))
                .Throw(new NotFoundException());

            var workstation = new WorkstationRequestModel()
            {
                Name = "01-02-01-01",
                Active = true
            };

            var data = await _controller.Put(workstation);

            await _workstationService.Received(1).Update("01-02-01-01", Arg.Any<WorkstationRequestModel>());

            Assert.IsType<NotFoundObjectResult>(data);
        }

        [Fact]
        public async Task If_Update_WithInvalidData_Return_StatusCode()
        {
            _workstationService
               .When(x => x.Update(Arg.Any<string>(), Arg.Any<WorkstationRequestModel>()))
               .Throw(new Exception());

            var workstation = new WorkstationRequestModel()
            {
                Name = "01-02-01-01",
                Active = true
            };

            var data = await _controller.Put(workstation);

            await _workstationService.Received(1).Update("01-02-01-01", Arg.Any<WorkstationRequestModel>());

            Assert.IsType<StatusCodeResult>(data);
            Assert.Equal(500, ((StatusCodeResult)data).StatusCode);
        }

        [Fact]
        public async Task If_Update_WithInvalidData_Return_BadRequest()
        {
            _workstationService
               .When(x => x.Update(Arg.Any<string>(), Arg.Any<WorkstationRequestModel>()))
               .Throw(new CustomValidationException());

            var workstation = new WorkstationRequestModel()
            {
                Name = "01-02-01-01",
                Active = true
            };

            var data = await _controller.Put(workstation);

            await _workstationService.Received(1).Update("01-02-01-01", Arg.Any<WorkstationRequestModel>());

            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task If_Get_Inactives_By_Floor_Return_Ok()
        {
            var response = await _controller.GetInactivesByFloor(2);

            await _workstationService.Received(1).GetInactivesByFloor(2);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task If_GetInactivesByFloor_WithInvalidData_Return_StatusCode()
        {
            _workstationService
               .When(x => x.GetInactivesByFloor(2))
               .Throw(new Exception());

            var data = await _controller.GetInactivesByFloor(2);

            Assert.IsType<StatusCodeResult>(data);
        }

        [Fact]
        public async Task If_GetInactivesByFloor_WithInvalidData_Return_BadRequest()
        {
          _workstationService
             .When(x => x.GetInactivesByFloor(2))
             .Throw(new NotFoundException());

          var data = await _controller.GetInactivesByFloor(2);

          Assert.IsType<BadRequestObjectResult>(data);
        }
    }
}
