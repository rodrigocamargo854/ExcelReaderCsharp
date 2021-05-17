using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Sentry;

namespace CadastroProdutos.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
               .UseContentRoot(Directory.GetCurrentDirectory())
               .ConfigureLogging((hostingContext, logging) =>
               {
                   // Requires `using Microsoft.Extensions.Logging;`
                   logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

                   logging.ClearProviders();
                   logging.AddLog4Net();

                   if (!string.IsNullOrWhiteSpace(SentryEnvInfoProvider.Dsn))
                   {
                       logging.AddSentry(options =>
                       {
                           options.Dsn = SentryEnvInfoProvider.Dsn;
                           options.ConfigureScope(scope => scope.SetTag("service.name", "cadastro_produtos"));
                           options.Release = $"cadastro_produtos@{SentryEnvInfoProvider.SERVICE_VERSION}";
                           options.Environment = Environment.GetEnvironmentVariable("DATADOG_ENV");
                       });
                   }
               })
               .Build()
               .RegisterEventConsumers()
               .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(options => { })
                .UseStartup<Startup>();
    }
}
