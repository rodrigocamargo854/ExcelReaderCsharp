using System;

namespace HBSIS.ReservaMesas.Application.Models
{
    public class CsvReportModel
    {
        public DateTime Data { get; set; }
        public string Andar { get; set; }
        public string Mesa { get; set; }
        public string Usuario { get; set; }
        public string Status { get; set; }

        public CsvReportModel(DateTime data, string andar, string mesa, string usuario, string status)
        {
            Data = data;
            Andar = andar;
            Mesa = mesa;
            Usuario = usuario;
            Status = status;
        }
    }
}
