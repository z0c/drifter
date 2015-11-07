using System;
using System.Collections.Generic;
using System.Linq;
using Drifter.Utilities;
using Newtonsoft.Json.Linq;

namespace Drifter.Scrapers
{
    public class WebPostScraper : IScrapePosts
    {
        private readonly IWebClient _webClient;

        public WebPostScraper(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public IEnumerable<Post> ScrapePosts(Uri uri)
        {
            var response = _webClient.DownloadString(uri);
            var json = JObject.Parse(response);

            return json["data"]["children"]
                   .Select(p => p["data"]
                   .ToObject<Post>());
        }
    }

    public interface IScrapePosts
    {
        IEnumerable<Post> ScrapePosts(Uri uri);
    }
}
