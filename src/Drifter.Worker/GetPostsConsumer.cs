using System;
using System.Threading.Tasks;
using Drifter.Indexers;
using Drifter.Messages;
using EasyNetQ.AutoSubscribe;
using log4net;

namespace Drifter.Worker
{
    class GetPostsConsumer : IConsumeAsync<GetPostsMessage>
    {
        private static ILog _logger;
        private readonly IPostIndexer _postIndexer;

        public GetPostsConsumer(ILog logger, IPostIndexer postIndexer)
        {
            _logger = logger;
            _postIndexer = postIndexer;
        }

        public Task Consume(GetPostsMessage message)
        {
            return Task.Factory.StartNew(() => {
                var posts = _postIndexer.GetPosts(new Uri(message.Uri));
                foreach (var post in posts)
                    _logger.Info(post.Url);
            });
        }
    }
}
