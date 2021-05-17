using System.Collections.Generic;
using HBSIS.ReservaMesas.Domain.Entities;

namespace HBSIS.ReservaMesas.Persistence.Seeds.Data
{
    public class FloorList
    {
        public List<Floor> Data { get; set; }

        public FloorList()
        {
            Data = new List<Floor>()
            {
                new Floor("Andar 1", false, "01-01", 1),
                new Floor("Andar 2", true, "01-02", 1),
                new Floor("Andar 3", true, "01-03", 1),
                new Floor("Andar 4", false, "01-04", 1),
                new Floor("Andar 5", false, "01-05", 1),
                new Floor("Andar 6", false, "01-06", 1),
                new Floor("Andar 7", false, "01-07", 1),
                new Floor("Andar 8", false, "01-08", 1),
                new Floor("Andar 9", false, "01-09", 1),
                new Floor("Andar 10", false, "01-10", 1)
            };
        }
    }
}
