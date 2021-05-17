using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory;
using HBSIS.ReservaMesas.Persistence.Context;
using HBSIS.ReservaMesas.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace HBSIS.ReservaMesas.IntegrationTests.Utils
{
    [ExcludeFromCodeCoverage]
    [Collection("Non-Parallel")]
    public abstract class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _webApplicationFactory;
        protected readonly HttpClient HttpClient;

        protected IntegrationTestBase(CustomWebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            HttpClient = webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        protected async Task<T> GetResponseBody<T>(HttpResponseMessage response)
        {
            var responseBodyJson = await response.Content.ReadAsStringAsync();
            var parsedResponseBody = JsonConvert.DeserializeObject<T>(responseBodyJson);
            return parsedResponseBody;
        }

        public StringContent CreateStringContent(object obj)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
