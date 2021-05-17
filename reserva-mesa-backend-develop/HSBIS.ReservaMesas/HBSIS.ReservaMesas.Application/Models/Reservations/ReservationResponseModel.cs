using System;

namespace HBSIS.ReservaMesas.Application.Models.Reservations
{
    public class ReservationResponseModel
    {
        public int Id { get; set; }
        public string WorkstationName { get; set; }
        public DateTime Date { get; set; }
        public DateTime CanceledOn { get; set; }

        public ReservationResponseModel(string workstationName, DateTime date, int id)
        {
            this.Id = id;
            this.WorkstationName = workstationName;
            this.Date = date;
        }
    }
}
