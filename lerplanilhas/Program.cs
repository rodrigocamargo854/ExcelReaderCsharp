using System;
using System.Linq;
using ClosedXML.Excel;

namespace lerplanilhas
{
    class Program
    {
        static void Main(string[] args)
        {
           var xls = new XLWorkbook(@"C:\Temp\ExemploExcel.xlsx");
           var planilha = xls.Worksheets.First(w => w.Name == "Planilha1");
            var totalLinhas = planilha.Rows().Count();
            // primeira linha é o cabecalho
          
        }
    }
}
