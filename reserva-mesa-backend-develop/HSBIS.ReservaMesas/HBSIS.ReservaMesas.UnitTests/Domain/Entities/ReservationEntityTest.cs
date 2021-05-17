using FluentAssertions;
using HBSIS.ReservaMesas.Domain.Entities;
using System;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Domain.Entities
{
    public class ReservationEntityTest
    {
        [Fact]
        public void Should_Create_Reservation_With_Constructor()
        {
            var data = new DateTime(2020,08,10).Date;
            var reservation = new Reservation(1, data, "teste", "name teste");

            reservation.WorkstationId.Should().Be(1);
            reservation.Date.Should().Be(data);
            reservation.UserId.Should().Be("teste");
        }

        [Fact]
        public void Should_Check_in()
        {
            var data = new DateTime(2020, 08, 10).Date;
            var reservation = new Reservation(1, data, "teste", "teste");

            reservation.CheckIn();
            reservation.CheckInStatus.Should().BeTrue();
            reservation.CheckInDateTime.Should().BeCloseTo(DateTime.Now, 20, " data de checkin deve ser proxima da atual por que ela é a partir da data de chamada do metodo", reservation.CheckInDateTime);
        }
    }
}
