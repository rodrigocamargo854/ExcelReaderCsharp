using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Floors;
using HBSIS.ReservaMesas.IntegrationTests.Controllers.Unity;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Persistence.Seeds.Data;
using HBSIS.ReservaMesas.Web;
using Xunit;

namespace HBSIS.ReservaMesas.IntegrationTests.Controllers.Floors
{
    public class FloorTest : IntegrationTestBase
    {
        private readonly UnityTestSetup _unityTestSetup;
        private readonly FloorTestSetup _floorTestSetup;

        public FloorTest(CustomWebApplicationFactory<Startup> customWebApplicationFactory) : base(customWebApplicationFactory)
        {
            _unityTestSetup = new UnityTestSetup(customWebApplicationFactory);
            _floorTestSetup = new FloorTestSetup(customWebApplicationFactory);
        }

        [Fact]
        public async Task Should_Get_All_Floors_By_Unity_Id()
        {
            var units = await _unityTestSetup.GetUnits();

            var response = await HttpClient.GetAsync($"../api/floors/units/{units[0].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Get_Floors_Of_Not_Registered_Unity()
        {
            var response = await HttpClient.GetAsync($"../api/floors/units/-1");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Get_Floor_By_Code()
        {
            var floorCode = new FloorList().Data.Where(x => x.Active).ToList()[0].Code;
            var response = await HttpClient.GetAsync($"../api/floors?code={floorCode}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Not_Found_On_Get_Floor_By_Code()
        {
            var response = await HttpClient.GetAsync("../api/floors?code=-1");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Not_Found_UnityId_On_Get_Floors_By_Id()
        {
            var response = await HttpClient.GetAsync("../api/floors/units/-189231");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Update_Floor()
        {
            var units = await _unityTestSetup.GetUnits();
            var floors = await _floorTestSetup.GetAllFloorByUnityId(units[0].Id);
            var requestModel = new FloorRequestModel
            {
                Active = true,
                Name = "Andar 1",
                UnityId = units[0].Id
            };

            var response = await HttpClient.PutAsync($"../api/floors/{floors[0].Id}", CreateStringContent(requestModel));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Not_Found_When_Throw_Not_Found_Exception()
        {
            var requestModel = new FloorRequestModel
            {
                Active = true,
                Name = "Andar 1",
                UnityId = 1
            };

            var response = await HttpClient.PutAsync("../api/floors/-1", CreateStringContent(requestModel));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Throw_Custom_Validation_Exception()
        {
            var units = await _unityTestSetup.GetUnits();
            var floors = await _floorTestSetup.GetAllFloorByUnityId(units[0].Id);
            var requestModel = new FloorRequestModel
            {
                Active = true,
                Name = "",
                UnityId = 1
            };

            var response = await HttpClient.PutAsync($"../api/floors/{floors[0].Id}", CreateStringContent(requestModel));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task Should_Return_Not_Found_When_Throw_Not_Found_Exception_On_Get_By_Id()
        {
            var response = await HttpClient.GetAsync("../api/floors/-1");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Get_By_Id()
        {
            var units = await _unityTestSetup.GetUnits();
            var floors = await _floorTestSetup.GetAllFloorByUnityId(units[0].Id);

            var response = await HttpClient.GetAsync($"../api/floors/{floors[0].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
