using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using log4net;
using Newtonsoft.Json.Linq;

namespace Drifter.Scrapers
{
    public class WebPostScraper : IScrapePosts
    {
        public IEnumerable<Post> ScrapePosts(Uri uri)
        {
            var client = new WebClient();
            var response = client.DownloadString(uri);
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
