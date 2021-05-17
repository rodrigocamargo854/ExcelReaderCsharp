using System.Collections.Generic;
using HBSIS.ReservaMesas.Domain.Entities;

namespace HBSIS.ReservaMesas.Persistence.Seeds.Data
{
    public class UnityList
    {
        public List<Unity> Data { get; set; }

        public UnityList()
        {
            Data = new List<Unity>()
            {
                new Unity("Blumenau", true), 
                new Unity("Maringá", false), 
                new Unity("Sorocaba", false)
            };
        }
    }
}
