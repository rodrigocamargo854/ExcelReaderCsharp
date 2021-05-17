using HBSIS.ReservaMesas.Application.Emails;
using HBSIS.ReservaMesas.Domain.Exceptions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HBSIS.ReservaMesas.Application.BackgroundServices
{
    public class SendEmailByBackgroundService : ISendEmailByBackgroundService, IHostedService
    {
        private readonly IEmailReservationAlertService _emailAlert;
        private readonly ILogger<SendEmailByBackgroundService> _logger;

        public SendEmailByBackgroundService(ILogger<SendEmailByBackgroundService> logger,
            IEmailReservationAlertService emailAlert)
        {
            _logger = logger;
            _emailAlert = emailAlert;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var cancellationSource = new CancellationTokenSource();

            Task.Run(() => DailyWorker(12, 00, 00, () => SendEmailInAdvance(), cancellationSource.Token));

            Task.Run(() => DailyWorker(10, 00, 00, () => SendEmailAlertDoesntAttended(), cancellationSource.Token));

            return Task.CompletedTask;
        }

        public void SendEmailInAdvance()
        {
            _emailAlert.SendEmailAlertOneDayBefore();
        }

        public void SendEmailAlertDoesntAttended()
        {
            _emailAlert.SendEmailAlertDoesntAttended();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Task Stoped");
            return Task.CompletedTask;
        }

        private void DailyWorker(int hour, int min, int sec, Action work, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var scanDateTime = new DateTime(
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        hour,
                        min,
                        sec);

                    TimeSpan ts;
                    if (scanDateTime > DateTime.Now)
                    {
                        ts = scanDateTime - DateTime.Now;
                    }
                    else
                    {
                        scanDateTime = scanDateTime.AddDays(1);
                        ts = scanDateTime - DateTime.Now;
                    }

                    try
                    {
                        Task.Delay(ts).Wait(token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }

                    work();
                }
            }
            catch (Exception)
            {
                throw new CustomValidationException("Não foi possível realizar ação. Favor contatar administrador.");
            }
        }
    }
}
