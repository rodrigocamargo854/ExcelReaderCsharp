using System;
using System.Collections.Generic;
using Br.Com.Hbsis.Rpc.Produtos;
using CadastroProdutos.WebApi.EventConsumers;
using Messaging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CadastroProdutos.WebApi
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHost RegisterEventConsumers(this IWebHost webHost)
        {
            var eventManager = webHost.Services.GetService<IEventManager>();
            var applicationConsumers = webHost.Services.GetService<IConsumerRegistry>();

            try
            {
                var consumers = new List<IConsumer>()
            {
                eventManager.Subscribe(QueueNameFor<ProdutoIncluido>(), webHost.Services.GetService<ProdutoIncluidoEventConsumer>()),
                // or
                // eventManager.Subscribe(QueueNameFor<CadastroProdutosEvent>(), webHost.Services.GetService<CadastroProdutosEventConsumer>()),
            };

                foreach (var consumer in consumers)
                {
                    applicationConsumers.Register(consumer);
                }
            }
            catch (Exception exception)
            {
                var logger = webHost.Services.GetService<ILogger<IWebHost>>();
                logger.LogError(exception, "Erro ao registrar consumidores de eventos");
                throw;
            }

            return webHost;
        }

        private static string QueueNameFor<TEvent>(string geo = null)
        {
            geo = !string.IsNullOrWhiteSpace(geo) ? $"{geo}_" : string.Empty;
            return $"cadastro_produtos_rodrigo.godoy_{geo}{typeof(TEvent).Name.ToLower()}";
        }
    }
}