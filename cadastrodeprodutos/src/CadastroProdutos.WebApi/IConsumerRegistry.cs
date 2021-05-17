using System.Collections.Generic;
using Messaging;

namespace CadastroProdutos.WebApi
{
    public interface IConsumerRegistry
    {
        void Register(IConsumer consumer);
        IEnumerable<IConsumer> GetConsumers();
    }
}