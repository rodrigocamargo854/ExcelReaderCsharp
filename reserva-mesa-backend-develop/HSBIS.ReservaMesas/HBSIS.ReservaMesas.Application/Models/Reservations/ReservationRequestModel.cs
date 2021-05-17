using System;

namespace HBSIS.ReservaMesas.Application.Models.Reservations
{
    public class ReservationRequestModel
    {
        public string WorkstationName { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
