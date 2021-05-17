using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models;
using System;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Application.Models
{
    public class CsvConfirmedWorkstationsReportModelTest
    {
        [Fact]
        public void Should_Create_Report_Model_With_Constructor()
        {
            var reportModel = new CsvConfirmedWorkstationsReportModel(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day),
                    "Andar Teste", "01-01-02-01", "teste@ambevtech.com.br");

            reportModel.Data.Should().Be(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day));
            reportModel.Andar.Should().Be("Andar Teste");
            reportModel.Mesa.Should().Be("01-01-02-01");
            reportModel.Usuario.Should().Be("teste@ambevtech.com.br");
        }
    }
}
