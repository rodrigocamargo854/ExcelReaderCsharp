using FluentAssertions;
using HBSIS.ReservaMesas.Domain.Entities;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Domain.Entities
{
    public class WorkstationEntityTest
    {
        [Fact]
        public void Should_Create_Workstation_With_Constructor()
        {
            var workstation = new Workstation("01-02-01-01", false, 1);

            workstation.Name.Should().Be("01-02-01-01");
            workstation.Active.Should().BeFalse();
            workstation.FloorId.Should().Be(1);
        }
    }
}
