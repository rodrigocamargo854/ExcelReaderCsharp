using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HBSIS.ReservaMesas.Application.Models.Unity;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Web;
using Xunit;

namespace HBSIS.ReservaMesas.IntegrationTests.Controllers.Unity
{
    public class UnityTest : IntegrationTestBase
    {
        public UnityTest(CustomWebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory)
        {
        }

        [Fact]
        public async Task Should_Get_Units()
        {
            var response = await HttpClient.GetAsync("../api/units");
            var responseBody = await GetResponseBody<UnityResponseModel[]>(response);

            responseBody.Should().Contain(unity => unity.Name == "Blumenau");
            responseBody.Should().Contain(unity => unity.Name == "Maringá");
            responseBody.Should().Contain(unity => unity.Name == "Sorocaba");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
