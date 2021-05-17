using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Reservations;
using HBSIS.ReservaMesas.IntegrationTests.Controllers.Floors;
using HBSIS.ReservaMesas.IntegrationTests.Controllers.Unity;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Persistence.Seeds.Data;
using HBSIS.ReservaMesas.Web;
using Xunit;

namespace HBSIS.ReservaMesas.IntegrationTests.Controllers.Reservation
{
    [Collection("Non-Parallel")]
    public class ReservationTest : IntegrationTestBase
    {
        private readonly ReservationTestSetup _reservationTestSetup;
        private readonly FloorTestSetup _floorTestSetup;
        private readonly UnityTestSetup _unityTestSetup;

        public ReservationTest(CustomWebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory)
        {
            _reservationTestSetup = new ReservationTestSetup(webApplicationFactory);
            _floorTestSetup = new FloorTestSetup(webApplicationFactory);
            _unityTestSetup = new UnityTestSetup(webApplicationFactory);
        }

        private string ConvertDateTimeToAcceptableFormat(DateTime datetime) => $"{datetime.Month}.{datetime.Day}.{datetime.Year}";

        [Fact]
        public async Task Should_Get_All_Reserved_Workstations_From_Date_Interval_And_Floor()
        {
            var units = await _unityTestSetup.GetUnits();
            var floors = await _floorTestSetup.GetAllFloorByUnityId(units[0].Id);

            var formattedInitialDate = ConvertDateTimeToAcceptableFormat(DateTime.Now);
            var formattedFinalDate = ConvertDateTimeToAcceptableFormat(DateTime.Now.AddDays(5));

            var response = await HttpClient.GetAsync($"../api/reservations/workstations?initialDate={formattedInitialDate}&finalDate={formattedFinalDate}&floorId={floors[0].Id}");
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Get_All_Reserved_Workstations_From_Date_Interval_And_Floor()
        {
            var notExists = -1;
            var badFormattedInitialDate = $"01.01.0001";
            var badFormattedFinalDate = $"01.01.0001";
            var url = $"../api/reservations/workstations?initialDate={badFormattedInitialDate}&finalDate={badFormattedFinalDate}&floorId={notExists}";

            var response = await HttpClient.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Create_Reservations()
        {
            ReservationRequestModel requestModel = new ReservationRequestModel
            {
                FinalDate = DateTime.Now.AddDays(5),
                InitialDate = DateTime.Now,
                WorkstationName = new WorkstationList().Data[0].Name
            };
            var response = await HttpClient.PostAsync("../api/reservations", CreateStringContent(requestModel));

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Create_Reservations_With_Invalid_Date()
        {
            ReservationRequestModel requestModel = new ReservationRequestModel
            {
                FinalDate = new DateTime(),
                InitialDate = new DateTime(),
                WorkstationName = new WorkstationList().Data[0].Name
            };
            var response = await HttpClient.PostAsync("../api/reservations", CreateStringContent(requestModel));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Create_Reservations_In_Reserved_Workstations()
        {
            ReservationRequestModel requestModel = new ReservationRequestModel
            {
                FinalDate = DateTime.Now.AddDays(5),
                InitialDate = DateTime.Now,
                WorkstationName = new WorkstationList().Data[1].Name
            };

            await _reservationTestSetup.CreateReservations(requestModel);
            var response = await HttpClient.PostAsync("../api/reservations", CreateStringContent(requestModel));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Get_All_Reservations_By_Date_Interval()
        {
            var formattedInitialDate = ConvertDateTimeToAcceptableFormat(DateTime.Now);
            var formattedFinalDate = ConvertDateTimeToAcceptableFormat(DateTime.Now.AddDays(5));
            var url = $"../api/reservations?initialDate={formattedInitialDate}&finalDate={formattedFinalDate}&currentPage=1";
            var response = await HttpClient.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Get_All_Reservations_By_Date_Interval_With_Invalid_Dates()
        {
            var formattedInitialDate = ConvertDateTimeToAcceptableFormat(DateTime.Now.AddDays(1));
            var formattedFinalDate = ConvertDateTimeToAcceptableFormat(DateTime.Now);
            var url = $"../api/reservations?initialDate={formattedInitialDate}&finalDate={formattedFinalDate}&currentPage=1";
            var response = await HttpClient.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Generate_Confirmed_Workstations_Csv_Report()
        {
            var formattedInitialDate = ConvertDateTimeToAcceptableFormat(DateTime.Now);
            var formattedFinalDate = ConvertDateTimeToAcceptableFormat(DateTime.Now.AddDays(5));
            var url = $"../api/reservations/reports/confirmed-workstations?initialDate={formattedInitialDate}&finalDate={formattedFinalDate}";
            var response = await HttpClient.PostAsync(url, CreateStringContent(new { }));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Generate_Confirmed_Workstations_Csv_Report_With_Invalid_Date_Interval()
        {
            var formattedInitialDate = ConvertDateTimeToAcceptableFormat(DateTime.Now.AddDays(1));
            var formattedFinalDate = ConvertDateTimeToAcceptableFormat(DateTime.Now);
            var url = $"../api/reservations/reports/confirmed-workstations?initialDate={formattedInitialDate}&finalDate={formattedFinalDate}";
            var response = await HttpClient.PostAsync(url, CreateStringContent(new { }));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Generate_Reserved_Workstations_Csv_Report()
        {
            var formattedInitialDate = ConvertDateTimeToAcceptableFormat(DateTime.Now);
            var formattedFinalDate = ConvertDateTimeToAcceptableFormat(DateTime.Now.AddDays(5));

            var response = await HttpClient.PostAsync($"../api/reservations/reports/reserved-workstations?initialDate={formattedInitialDate}&finalDate={formattedFinalDate}", CreateStringContent(new { }));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Generate_Reserved_Workstations_Csv_Report_With_Invalid_Date_Interval()
        {
            var formattedInitialDate = ConvertDateTimeToAcceptableFormat(DateTime.Now.AddDays(1));
            var formattedFinalDate = ConvertDateTimeToAcceptableFormat(DateTime.Now);

            var response = await HttpClient.PostAsync($"../api/reservations/reports/reserved-workstations?initialDate={formattedInitialDate}&finalDate={formattedFinalDate}", CreateStringContent(new { }));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Delete_Reservations() 
        {
            ReservationRequestModel createReservationRequestModel = new ReservationRequestModel
            {
                FinalDate = DateTime.Now.AddDays(5),
                InitialDate = DateTime.Now,
                WorkstationName = new WorkstationList().Data[1].Name
            };
            var createReservationResponse = await _reservationTestSetup.CreateReservations(createReservationRequestModel);
            var cancelReservationRequestModel = new CancelReservationRequestModel()
            {
                ReservationIds = createReservationResponse.ToList()
            };

            var response = await HttpClient.PutAsync($"../api/reservations/delete", CreateStringContent(cancelReservationRequestModel));

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Should_Return_Not_Found_When_Delete_Not_Registered_Reservation()
        {
            var cancelReservationRequestModel = new CancelReservationRequestModel()
            {
                ReservationIds = new int[] { -1 }
            };

            var response = await HttpClient.PutAsync($"../api/reservations/delete", CreateStringContent(cancelReservationRequestModel));

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_When_Delete_Reservations_With_Validation_Errors()
        {
            var cancelReservationRequestModel = new CancelReservationRequestModel()
            {
                ReservationIds = new int[] { }
            };

            var response = await HttpClient.PutAsync($"../api/reservations/delete", CreateStringContent(cancelReservationRequestModel));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_Get_All_Active_Reservations_From_Today()
        {
            var response = await HttpClient.GetAsync("../api/reservations/check-in?currentPage=1&nameFilter=Test");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Check_In()
        {
            var response = await HttpClient.PutAsync("../api/reservations/check-in?reservationId=1", CreateStringContent(new { }));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
