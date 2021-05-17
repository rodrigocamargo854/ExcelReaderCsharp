using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Unity;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Web.Controllers
{
    public class UnityControllerTest
    {
        private readonly IUnityService _unityServiceMock;
        private readonly UnityController _unityController;

        public UnityControllerTest()
        {
            _unityServiceMock = Substitute.For<IUnityService>();
            _unityController = new UnityController(_unityServiceMock);
        }

        [Fact]
        public async Task Should_Get_All_Unities()
        {
            var insertedUnities = new List<UnityResponseModel>()
            {
                new UnityResponseModel { Name = "Blumenau", Active = true, Id = 1},
                new UnityResponseModel { Name = "Sorocaba", Active = false, Id = 2},
                new UnityResponseModel { Name = "Maringá", Active = false, Id = 3},
            };
            _unityServiceMock.GetAll().Returns(insertedUnities);

            var response = await _unityController.GetAll();

            await _unityServiceMock.Received(1).GetAll();
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Return_StatusCode_If_Exception_Was_Thrown()
        {
            _unityServiceMock.When(service => service.GetAll()).Throw(new Exception());

            var response = await _unityController.GetAll();

            await _unityServiceMock.Received(1).GetAll();
            response.Should().BeOfType<StatusCodeResult>();
        }

        [Fact]
        public async Task Should_Return_BadRequest_If_Exception_Was_Thrown()
        {
            _unityServiceMock.When(service => service.GetAll()).Throw(new NotFoundException());

            var response = await _unityController.GetAll();

            await _unityServiceMock.Received(1).GetAll();
            response.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
