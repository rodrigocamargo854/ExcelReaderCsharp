using System.Diagnostics.CodeAnalysis;
using HBSIS.ReservaMesas.Domain.Configurations;
using HBSIS.ReservaMesas.Persistence.Context;
using HBSIS.ReservaMesas.Persistence.Seeds;
using HBSIS.ReservaMesas.Web.DependencyInjection.Application;
using HBSIS.ReservaMesas.Web.DependencyInjection.HostedServices;
using HBSIS.ReservaMesas.Web.DependencyInjection.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HBSIS.ReservaMesas.Web
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddDbContext(services);

            new Services().AddServicesDependencyInjection(services);
            new Repositories().AddRepositoriesDependencyInjection(services);
            new HostedServices().AddHostedServicesDependencyInjection(services, Configuration.GetSection("SmtpClient").Get<EmailConfiguration>(), Configuration.GetSection("UrlConfig").Get<UrlConfiguration>());

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options =>
                {
                    Configuration.Bind("AzureAd", options);
                });

            services.AddControllers();
            services.AddCors(o => o.AddPolicy("CORS", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CORS");

            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                MainContext context = serviceScope
                    .ServiceProvider
                    .GetRequiredService<MainContext>();

                SeedInitialData(context);
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SeedInitialData(MainContext context)
        {
            new InitialUnitySeed().SeedData(context);
            new InitialFloorSeed().SeedData(context);
            new InitialWorkstationSeed().SeedData(context);
        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
