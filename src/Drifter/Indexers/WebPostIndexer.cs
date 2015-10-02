using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using log4net;
using Newtonsoft.Json.Linq;

namespace Drifter.Indexers
{
    public class WebPostIndexer : IPostIndexer
    {
        private static ILog _logger;

        public WebPostIndexer(ILog logger)
        {
            _logger = logger;
        }

        public IEnumerable<Post> GetPosts(Uri uri)
        {
            var client = new WebClient();
            var response = client.DownloadString(uri);
            var json = JObject.Parse(response);

            return json["data"]["children"]
                   .Select(p => p["data"]
                   .ToObject<Post>());
        }
    }

    public interface IPostIndexer
    {
        IEnumerable<Post> GetPosts(Uri uri);
    }
}
