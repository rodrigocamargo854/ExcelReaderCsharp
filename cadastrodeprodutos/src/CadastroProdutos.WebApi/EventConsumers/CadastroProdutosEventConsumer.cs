using Microsoft.Extensions.Logging;
using CadastroProdutos.Eventos;

namespace CadastroProdutos.WebApi.EventConsumers
{
    public class CadastroProdutosEventConsumer : AbstractEventConsumer<CadastroProdutosEvent>
    {
        private readonly ILogger<CadastroProdutosEventConsumer> _logger;

        public CadastroProdutosEventConsumer(ILogger<CadastroProdutosEventConsumer> logger)
        {
            _logger = logger;
        }

        protected override void ConsumeEvent(CadastroProdutosEvent evento)
        {
            // TODO implementar
        }
    }
}