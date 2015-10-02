using System.Threading.Tasks;
using Drifter.Messages;
using EasyNetQ.AutoSubscribe;
using log4net;

namespace Drifter.Worker
{
    class Testing123Consumer : IConsumeAsync<Testing123Message>
    {
        private static ILog _logger;

        public Testing123Consumer(ILog logger)
        {
            _logger = logger;
        }

        public Task Consume(Testing123Message message)
        {
            return Task.Factory.StartNew(() => _logger.Info("Testing 1 2 3"));
        }
    }
}
