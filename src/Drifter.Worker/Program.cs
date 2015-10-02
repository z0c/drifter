using System;
using System.Configuration;
using System.Reflection;
using Drifter.Worker.DI;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;

namespace Drifter.Worker
{
    class Program
    {
// ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            LoggerConfig.ConfigureLogger();
            var container = UnityConfig.RegisterComponents();

            RabbitHutch.SetContainerFactory(() => new UnityAdapter(container));

            using (var bus = RabbitHutch.CreateBus(ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString))
            {
                var autoSubscriber = new AutoSubscriber(bus, "Worker")
                {
                    AutoSubscriberMessageDispatcher = new UnityMessageDispatcher(container)
                };

                autoSubscriber.Subscribe(Assembly.GetExecutingAssembly());
                autoSubscriber.SubscribeAsync(Assembly.GetExecutingAssembly());

                Console.ReadLine();
            }
        }
    }
}
