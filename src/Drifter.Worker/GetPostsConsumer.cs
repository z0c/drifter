using System;
using System.Threading.Tasks;
using Drifter.Scrapers;
using Drifter.Messages;
using EasyNetQ.AutoSubscribe;
using log4net;

namespace Drifter.Worker
{
    class GetPostsConsumer : IConsumeAsync<GetPostsMessage>
    {
        private static ILog _logger;
        private readonly IScrapePosts _postScraper;

        public GetPostsConsumer(ILog logger, IScrapePosts postScraper)
        {
            _logger = logger;
            _postScraper = postScraper;
        }

        public Task Consume(GetPostsMessage message)
        {
            return Task.Factory.StartNew(() => {
                var posts = _postScraper.ScrapePosts(new Uri(message.Uri));
                foreach (var post in posts)
                    _logger.Info(post.Url);
            });
        }
    }
}
