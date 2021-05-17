namespace HBSIS.ReservaMesas.Application.Models.Workstations
{
    public class WorkstationResponseModel : BaseEntityModel
    {
        public int Id { get; set; }
        public int FloorId { get; set; }

        public WorkstationResponseModel(int id, string name, bool active, int floorId)
        {
            this.Id = id;
            this.Name = name;
            this.Active = active;
            this.FloorId = floorId;
        }
    }
}
