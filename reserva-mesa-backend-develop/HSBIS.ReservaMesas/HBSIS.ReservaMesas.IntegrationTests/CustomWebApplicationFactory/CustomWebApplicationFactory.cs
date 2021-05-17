using System;
using System.Linq;
using HBSIS.ReservaMesas.IntegrationTests.Utils;
using HBSIS.ReservaMesas.Persistence.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HBSIS.ReservaMesas.IntegrationTests.CustomWebApplicationFactory
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public MainContext DatabaseContext;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor serviceDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<MainContext>));

                if (serviceDescriptor != null)
                {
                    services.Remove(serviceDescriptor);
                }

                ConfigureTestDatabase(services);

                ServiceProvider serviceProvider = services.BuildServiceProvider();

                var scope = serviceProvider.CreateScope();

                IServiceProvider scopedServices = scope.ServiceProvider;
                DatabaseContext = scopedServices.GetRequiredService<MainContext>();
                ILogger logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                DatabaseContext.Database.EnsureCreated();
                services.AddAuthentication("Test").AddScheme<AuthenticationSchemeOptions, FakeAuthentication>("Test", options => { });
           });
        }

        private void ConfigureTestDatabase(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDatabase");
            });
        }

    }
}
