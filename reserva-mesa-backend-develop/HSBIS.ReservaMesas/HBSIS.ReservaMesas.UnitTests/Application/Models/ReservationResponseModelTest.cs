using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Reservations;
using System;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.Models
{
    public class ReservationResponseModelTest
    {
       [Fact]
       public void Should_Create_Response_Model_With_Constructor()
       {
          var responseModel = new ReservationResponseModel("teste", new DateTime(), 1);

          responseModel.WorkstationName.Should().Be("teste");
          responseModel.Date.Should().Be(default(DateTime));
          responseModel.Id.Should().Be(1);
       }
    }
}
