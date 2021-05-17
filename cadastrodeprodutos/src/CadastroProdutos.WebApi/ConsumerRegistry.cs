using System.Collections.Generic;
using Messaging;

namespace CadastroProdutos.WebApi
{
    public class ConsumerRegistry : IConsumerRegistry
    {
        private readonly List<IConsumer> _consumers = new List<IConsumer>();

        public void Register(IConsumer consumer)
        {
            _consumers.Add(consumer);
        }

        public IEnumerable<IConsumer> GetConsumers()
        {
            return _consumers;
        }
    }
}