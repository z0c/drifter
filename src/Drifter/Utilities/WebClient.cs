using System;

namespace Drifter.Utilities
{
    public class WebClient : IWebClient
    {
        public string DownloadString(Uri uri)
        { 
            var client = new WebClient();
            return client.DownloadString(uri);
        }
    }

    public interface IWebClient
    {
        string DownloadString(Uri uri);
    }
}
