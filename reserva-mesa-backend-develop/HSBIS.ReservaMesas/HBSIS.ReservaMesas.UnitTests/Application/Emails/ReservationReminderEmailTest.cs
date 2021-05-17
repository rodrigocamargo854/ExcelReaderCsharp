using FluentAssertions;
using HBSIS.ReservaMesas.Application.Emails;
using HBSIS.ReservaMesas.Domain.Configurations;
using HBSIS.ReservaMesas.Domain.Entities;
using HBSIS.ReservaMesas.UnitTests.Domain.EntitiesForTest;
using NSubstitute;
using System;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.Emails
{
    public class ReservationReminderEmailTest
    {
        private readonly IReservationReminderEmail _reservationReminderEmail;
        private readonly UrlConfiguration _urlConfiguration;

        public ReservationReminderEmailTest()
        {
            _urlConfiguration = Substitute.For<UrlConfiguration>();
            _reservationReminderEmail = new ReservationReminderEmail(_urlConfiguration);
        }

        [Fact]
        public void Should_Create_Mail_Message()
        {
            _urlConfiguration.Development = "http://localhost:5000/";

            var workstation = new WorkstationForTest(2, "01-02-01-02", true, new Floor("Andar 2", true, "01-01", 1));
            var reservation = new ReservationForTest(1, "vitinho.ligadaslendas@gmail.com", "test", DateTime.Today.AddDays(1), workstation);

            var createEmail = _reservationReminderEmail.CreateEmailMessage(reservation, DateTime.Today);

            createEmail.Should().NotBeNull();
        }
    }
}
