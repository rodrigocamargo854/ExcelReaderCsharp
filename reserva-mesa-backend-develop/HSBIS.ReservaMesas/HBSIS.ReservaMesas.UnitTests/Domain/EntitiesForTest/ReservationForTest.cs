using HBSIS.ReservaMesas.Domain.Entities;
using System;

namespace HBSIS.ReservaMesas.UnitTests.Domain.EntitiesForTest
{
    public class ReservationForTest : Reservation
    {
        public ReservationForTest(int workstationId, string userId, string userName, DateTime date, Workstation workstation) : base(workstationId, date, userId, userName)
        {
            this.Workstation = workstation;
        }
    }
}
