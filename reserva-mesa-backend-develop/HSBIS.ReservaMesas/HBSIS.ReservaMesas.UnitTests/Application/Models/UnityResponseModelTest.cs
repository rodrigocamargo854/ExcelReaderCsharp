using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Unity;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.Models
{
    public class UnityResponseModelTest
    {
        [Fact]
        public void Should_Create_Unity_Response_Model()
        {
            var responseModel = new UnityResponseModel { Id = 1, Name = "teste", Active = true };

            responseModel.Id.Should().Be(1);
            responseModel.Name.Should().Be("teste");
            responseModel.Active.Should().Be(true);
        }
    }
}
