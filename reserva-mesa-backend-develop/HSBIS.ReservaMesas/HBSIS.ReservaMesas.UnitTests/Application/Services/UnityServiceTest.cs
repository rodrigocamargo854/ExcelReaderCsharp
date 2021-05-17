using FluentAssertions;
using HBSIS.ReservaMesas.Application.Services;
using HBSIS.ReservaMesas.Application.Services.Interfaces;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.Domain.Exceptions;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.Services
{
    public class UnityServiceTest
    {
        private readonly IUnityRepository _unityRepositoryMock;
        private readonly IUnityService _unityService;

        public UnityServiceTest()
        {
            _unityRepositoryMock = Substitute.For<IUnityRepository>();
            _unityService = new UnityService(_unityRepositoryMock);
        }

        [Fact]
        public async Task Should_Get_All_Units()
        {
            var insertedUnities = new List<Unity> { new Unity("Blumenau", true), new Unity("Maringá", false), new Unity("Sorocaba", false) };
            _unityRepositoryMock.GetAll().Returns(insertedUnities);

            var responseModels = await _unityService.GetAll();

            await _unityRepositoryMock.Received(1).GetAll();
            responseModels.Should().HaveCount(3);
        }

        [Fact]
        public async Task Should_Not_Get_All_Units_When_Units_Are_Empty()
        {
            var insertedUnities = new List<Unity>();
            _unityRepositoryMock.GetAll().Returns(insertedUnities);

           var ex = await Assert.ThrowsAsync<NotFoundException>(async () =>
              await _unityService.GetAll());

          ex.Message.Should().Contain("Unidades não encontradas!");
        }

        [Fact]
        public async Task Should_Not_Get_All_Units_When_Units_Are_Null()
        {
          var insertedUnities = new List<Unity>();
          _unityRepositoryMock.GetAll().ReturnsNull();

          var ex = await Assert.ThrowsAsync<NotFoundException>(async () =>
             await _unityService.GetAll());

          ex.Message.Should().Contain("Unidades não encontradas!");
        }
    }
}
