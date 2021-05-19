using Microsoft.Extensions.Logging;
using CadastroProdutos.Eventos;
using Br.Com.Hbsis.Rpc.Produtos;

namespace CadastroProdutos.WebApi.EventConsumers
{
    public class ProdutoIncluidoEventConsumer : AbstractEventConsumer<ProdutoIncluido>
    {
        private readonly ILogger<ProdutoIncluidoEventConsumer> _logger;

        public ProdutoIncluidoEventConsumer(ILogger<ProdutoIncluidoEventConsumer> logger)
        {
            _logger = logger;
        }

        protected override void ConsumeEvent(ProdutoIncluido evento)
        {
            System.Console.WriteLine("Produto incluído: " + evento.Nome);
        }
    }
}