using HBSIS.ReservaMesas.Domain.Entities;

namespace HBSIS.ReservaMesas.UnitTests.Domain.EntitiesForTest
{
    public class WorkstationForTest : Workstation
    {
        public WorkstationForTest(int floorId, string name, bool active, Floor floor) : base(name, active, floorId)
        {
            this.Floor = floor;
        }
    }
}
