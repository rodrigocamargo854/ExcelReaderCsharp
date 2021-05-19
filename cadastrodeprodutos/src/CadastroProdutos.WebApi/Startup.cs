using System;
using Messaging;
using Messaging.Brokers.Rabbit;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OpenTracing;
using OpenTracing.Util;
using StatsdClient;
using CadastroProdutos.Dados.Mongo;
using Messaging.Brokers.Memory;
using Datadog.Trace.Configuration;
using CadastroProdutos.Dados.Mongo.Repositories;
using CadastroProdutos.WebApi.EventConsumers;


namespace CadastroProdutos.WebApi
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureInfra(services);
            ConfigureApplicationServices(services);

            services.AddControllers();
            services.AddSingleton<IRabbitInfoProvider, RabbitEnvInfoProvider>();
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                var serviceName = Environment.GetEnvironmentVariable("DD_SERVICE_NAME") ?? "cadastro_produtos";
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                ISampler sampler = new ConstSampler(sample: true);
                ITracer jaegerTracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();
                GlobalTracer.Register(jaegerTracer);

                return jaegerTracer;
            });
            services.AddOpenTracing();
            services.AddDataDog();
        }

        private void ConfigureInfra(IServiceCollection services)
        {
            services.AddSingleton<IRabbitInfoProvider, RabbitEnvInfoProvider>();
            var settings = TracerSettings.FromDefaultSources();
            settings.ServiceName = "CadastroProdutos";
            settings.AnalyticsEnabled = true;
            var datadogTracer = new Datadog.Trace.Tracer(settings);
            services.AddSingleton<IEventManager>(serviceProvider =>
            {
                switch (Environment.GetEnvironmentVariable("BROKER_TIPO"))
                {
                    case TipoBroker.Rabbit:
                        return new RabbitEventManager(
                            serviceProvider.GetService<IRabbitInfoProvider>(),
                            serviceProvider.GetService<ILogger<RabbitEventManager>>(),
                            datadogTracer,
                            serviceProvider.GetService<ILogger<DatadogTraceController>>()
                        );
                    default:
                        return new MemoryEventManager(serviceProvider.GetService<Datadog.Trace.Tracer>());
                }
            });
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddSingleton<IMongoInfoProvider, MongoEnvInfoProvider>();
            services.AddSingleton<IMongoDatabaseFactory, MongoDatabaseFactory>();
            services.AddSingleton<IConsumerRegistry, ConsumerRegistry>(); 
            services.AddTransient<ProdutoIncluidoEventConsumer>(); 
            services.AddScoped<IProdutoRepository, ProdutoRepository>(); 
            
            services.AddSingleton<IMongoDatabase>(serviceProvider =>
            {
                var mongoDatabaseFactory = serviceProvider.GetService<IMongoDatabaseFactory>();
                return mongoDatabaseFactory.GetDatabase();
            });
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            // TODO registrar serviços da aplicação
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            _logger.LogInformation("Rotas configuradas com sucesso");
        }
    }

    public static class DataDogService
    {
        public static void AddDataDog(this IServiceCollection services)
        {
            var host = Environment.GetEnvironmentVariable(StatsdConfig.DD_AGENT_HOST_ENV_VAR);
            if (string.IsNullOrWhiteSpace(host))
                return;

            DogStatsd.Configure(new StatsdConfig
            {
                StatsdServerName = host,
                StatsdPort = StatsdConfig.DefaultStatsdPort,
                Prefix = "hercules.cadastro_produtos"
            });
        }
    }
}
