using FluentAssertions;
using HBSIS.ReservaMesas.Domain.Entities;
using Xunit;

namespace HBSIS.ReservaMesas.UnitTests.Domain.Entities
{
    public class FloorEntityTest
    {
        [Fact]
        public void Should_Create_Floor_With_Constructor()
        {
            var floor = new Floor("Andar 3", true, "01-03", 1);

            floor.Name.Should().Be("Andar 3");
            floor.Active.Should().BeTrue();
            floor.UnityId.Should().Be(1);
        }
    }
}
