using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Microsoft.Practices.Unity;

namespace Drifter.Worker.DI
{
    public class UnityMessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        private readonly IUnityContainer _container;

        public UnityMessageDispatcher(IUnityContainer container)
        {
            _container = container;
        }

        public void Dispatch<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : IConsume<TMessage>
        {
            var consumer = _container.Resolve<TConsumer>();
            consumer.Consume(message);
        }

        public Task DispatchAsync<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : IConsumeAsync<TMessage>
        {
            var consumer = _container.Resolve<TConsumer>();
            return consumer.Consume(message);
        }
    }
}
