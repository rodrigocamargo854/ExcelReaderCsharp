using Messaging;
using log4net;

namespace CadastroProdutos.WebApi.EventConsumers
{
    public abstract class AbstractEventConsumer<T> : IEventConsumer<T>
    {
        public void Consume(T @event)
        {
            dynamic dynamicEvent = @event;
            using (ThreadContext.Stacks["event"].Push($"traceID {dynamicEvent.UberTraceId}"))
            {
                ConsumeEvent(@event);
            }
        }

        protected abstract void ConsumeEvent(T evento);
    }
}