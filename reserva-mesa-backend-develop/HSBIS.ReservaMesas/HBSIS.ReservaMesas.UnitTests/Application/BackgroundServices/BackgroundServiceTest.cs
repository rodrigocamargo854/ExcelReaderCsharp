using HBSIS.ReservaMesas.Application.BackgroundServices;
using HBSIS.ReservaMesas.Application.Emails;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.BackgroundServices
{
    public class BackgroundServiceTest
    {
        private readonly IEmailReservationAlertService _emailReservationAlert;
        private readonly ILogger<SendEmailByBackgroundService> _logger;
        private readonly ISendEmailByBackgroundService _sendEmailByBackgroundService;

        public BackgroundServiceTest()
        {
            _logger = Substitute.For<ILogger<SendEmailByBackgroundService>>();
            _emailReservationAlert = Substitute.For<IEmailReservationAlertService>();
            _sendEmailByBackgroundService = new SendEmailByBackgroundService(_logger, _emailReservationAlert);
        }

        [Fact]
        public async Task Should_Run_SendEmailInAdvance()
        {
            _sendEmailByBackgroundService.SendEmailInAdvance();

            await _emailReservationAlert.Received(1).SendEmailAlertOneDayBefore();
        }

        [Fact]
        public async Task Should_Run_SendEmailAlertDoesntAttended()
        {
            _sendEmailByBackgroundService.SendEmailAlertDoesntAttended();

            await _emailReservationAlert.Received(1).SendEmailAlertDoesntAttended();
        }
    }
}
