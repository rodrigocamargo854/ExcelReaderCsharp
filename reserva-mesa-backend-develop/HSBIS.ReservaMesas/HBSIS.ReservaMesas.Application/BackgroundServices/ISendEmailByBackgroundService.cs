namespace HBSIS.ReservaMesas.Application.BackgroundServices
{
    public interface ISendEmailByBackgroundService
    {
        void SendEmailInAdvance();
        void SendEmailAlertDoesntAttended();
    }
}
