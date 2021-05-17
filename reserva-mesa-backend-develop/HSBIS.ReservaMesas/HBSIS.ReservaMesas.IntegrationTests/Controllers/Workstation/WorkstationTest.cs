using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Workstations;
using HBSIS.ReservaMesas.IntegrationTests.Controllers.Floors;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Persistence.Seeds.Data;
using HBSIS.ReservaMesas.Web;
using Xunit;

namespace HBSIS.ReservaMesas.IntegrationTests.Controllers.Workstation
{
    public class WorkstationTest : IntegrationTestBase
    {
        private readonly FloorTestSetup _floorTestSetup;

        public WorkstationTest(CustomWebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory)
        {
            _floorTestSetup = new FloorTestSetup(webApplicationFactory);
        }

        [Fact]
        public async Task Should_Get_By_Name()
        {
            string workstationName = new WorkstationList().Data[0].Name;

            var response = await HttpClient.GetAsync($"../api/workstations/{workstationName}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Not_Found_When_Get_By_Name()
        {
            var response = await HttpClient.GetAsync("../api/workstations/not-exist");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Get_All_Inactives_By_Floor()
        {
            var floors = await _floorTestSetup.GetAllFloorByUnityId(1);
            var response = await HttpClient.GetAsync($"../api/workstations/inactives?floorId={floors[0].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Get_All_Inactives_By_Floor()
        {
            var response = await HttpClient.GetAsync("../api/workstations/inactives?floorId=-1");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Update_Workstation()
        {
            var requestModel = new WorkstationRequestModel
            {
                Active = false,
                Name = new WorkstationList().Data[0].Name
            };

            var response = await HttpClient.PutAsync("../api/workstations", CreateStringContent(requestModel));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Not_Found_On_Update_Workstation()
        {
            var requestModel = new WorkstationRequestModel
            {
                Active = false,
                Name = "Not exists"
            };

            var response = await HttpClient.PutAsync("../api/workstations", CreateStringContent(requestModel));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
