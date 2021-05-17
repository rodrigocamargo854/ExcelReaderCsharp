using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Floors;
using HBSIS.ReservaMesas.Application.Models.Unity;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Persistence.Seeds.Data;
using HBSIS.ReservaMesas.Web;
using Xunit;

namespace HBSIS.ReservaMesas.IntegrationTests.Seeds
{
    [ExcludeFromCodeCoverage]
    [Collection("Non-Parallel")]
    public class SeedsTest : IntegrationTestBase
    {
        public SeedsTest(CustomWebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory)
        {
        }

        private async Task<UnityResponseModel[]> GetUnits()
        {
            var response = await HttpClient.GetAsync("../api/units");
            return await GetResponseBody<UnityResponseModel[]>(response);
        }

        private async Task<FloorResponseModel[]> GetFloors()
        {
            var units = await GetUnits();
            var response = await HttpClient.GetAsync($"../api/floors/units/{units[0].Id}");
            var responseBody = await GetResponseBody<FloorResponseModel[]>(response);
            return responseBody;
        }

        [Fact]
        public async Task Should_Have_Items_In_Unity_Table()
        {
            var response = await GetUnits();

            response.Should().Contain(unity => unity.Name == "Blumenau");
            response.Should().Contain(unity => unity.Name == "Maringá");
            response.Should().Contain(unity => unity.Name == "Sorocaba");
        }

        [Fact]
        public async Task Should_Have_Items_In_Floor_Table()
        {
            var response = await GetFloors();

            response.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Should_Have_Items_In_Workstation_Table()
        {
            var workstationName = new WorkstationList().Data[0].Name;
            var responseJson = await HttpClient.GetAsync($"../api/workstations/{workstationName}");
            var parsedResponse = await GetResponseBody<FloorResponseModel>(responseJson);

            parsedResponse.Should().NotBeNull();
        }
    }
}
