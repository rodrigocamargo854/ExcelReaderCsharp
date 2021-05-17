using System.Threading.Tasks;
using HBSIS.ReservaMesas.Application.Models.Unity;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Web;

namespace HBSIS.ReservaMesas.IntegrationTests.Controllers.Unity
{
    public class UnityTestSetup : IntegrationTestBase
    {
        public UnityTestSetup(CustomWebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory)
        {
        }

        public async Task<UnityResponseModel[]> GetUnits() 
        {
            var response = await HttpClient.GetAsync("../api/units");
            var responseBody = await GetResponseBody<UnityResponseModel[]>(response);
            return responseBody;
        }
    }
}
