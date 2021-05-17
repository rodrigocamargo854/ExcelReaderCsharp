using System.Collections.Generic;
using System.Threading.Tasks;
using HBSIS.ReservaMesas.Application.Models.Reservations;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Web;

namespace HBSIS.ReservaMesas.IntegrationTests.Controllers.Reservation
{
    public class ReservationTestSetup : IntegrationTestBase
    {
        public ReservationTestSetup(CustomWebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory)
        {
        }

        public async Task<IEnumerable<int>> CreateReservations(ReservationRequestModel requestModel)
        {
            var response = await HttpClient.PostAsync("../api/reservations", CreateStringContent(requestModel));
            var responseBody = await GetResponseBody<IEnumerable<int>>(response);
            return responseBody;
        }
    }
}
