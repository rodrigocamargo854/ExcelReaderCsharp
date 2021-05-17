using System.Diagnostics.CodeAnalysis;
using HBSIS.ReservaMesas.Application.BackgroundServices;
using HBSIS.ReservaMesas.Application.Emails;
using HBSIS.ReservaMesas.Domain.Configurations;
using HBSIS.ReservaMesas.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HBSIS.ReservaMesas.Web.DependencyInjection.HostedServices
{
    [ExcludeFromCodeCoverage]
    public class HostedServices
    {
        public void AddHostedServicesDependencyInjection(IServiceCollection services, EmailConfiguration emailConfig, UrlConfiguration urlConfig)
        {
            var reservationRepository = services.BuildServiceProvider().GetService<IReservationRepository>();

            services.AddSingleton<IReservationReminderEmail>(services =>
                new ReservationReminderEmail(urlConfig));

            services.AddSingleton<IEmailReservationAlertService>(services =>
                new EmailReservationAlertService(reservationRepository,
                emailConfig,
                services.GetRequiredService<IReservationReminderEmail>()));

            services.AddSingleton<ISendEmailByBackgroundService, SendEmailByBackgroundService>();
        }
    }
}
