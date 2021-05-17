using System;

namespace HBSIS.ReservaMesas.Application.Models
{
    public class CsvConfirmedWorkstationsReportModel
    {
        public DateTime Data { get; set; }
        public string Andar { get; set; }
        public string Mesa { get; set; }
        public string Usuario { get; set; }

        public CsvConfirmedWorkstationsReportModel(DateTime data, string andar, string mesa, string usuario)
        {
            Data = data;
            Andar = andar;
            Mesa = mesa;
            Usuario = usuario;
        }
    }
}
