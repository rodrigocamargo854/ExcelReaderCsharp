using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace HBSIS.ReservaMesas.Application.Reports
{
    public class CsvReportsManager
    {
        public byte[] GenerateCsvReport<T>(IEnumerable<T> records) where T : class
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                {
                    csvWriter.WriteRecords<T>(records);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
