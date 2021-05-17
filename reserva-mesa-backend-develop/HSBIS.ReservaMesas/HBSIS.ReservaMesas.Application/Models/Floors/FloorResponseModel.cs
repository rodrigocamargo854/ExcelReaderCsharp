namespace HBSIS.ReservaMesas.Application.Models.Floors
{
    public class FloorResponseModel : BaseEntityModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int UnityId { get; set; }

        public FloorResponseModel(int id, string name, bool active, string code, int unityId)
        {
            Id = id;
            Name = name;
            Active = active;
            Code = code;
            UnityId = unityId;
        }
    }
}
