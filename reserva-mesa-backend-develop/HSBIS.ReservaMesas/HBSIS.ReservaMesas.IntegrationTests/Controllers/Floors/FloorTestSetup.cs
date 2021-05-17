using System.Threading.Tasks;
using HBSIS.ReservaMesas.Application.Models.Floors;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Web;

namespace HBSIS.ReservaMesas.IntegrationTests.Controllers.Floors
{
    class FloorTestSetup : IntegrationTestBase
    {
        public FloorTestSetup(CustomWebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory)
        {
        }

        public async Task<FloorResponseModel[]> GetAllFloorByUnityId(int unityId) 
        {
            var response = await HttpClient.GetAsync($"../api/floors/units/{unityId}");
            var responseBody = await GetResponseBody<FloorResponseModel[]>(response);
            return responseBody;
        }
    }
}
