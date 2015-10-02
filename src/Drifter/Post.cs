using System;
using Drifter.JsonConverters;
using Newtonsoft.Json;

namespace Drifter
{
    public class Post
    {
        public string Id { get; set; }

        public string Domain { get; set; }
        
        public string Title { get; set; }
        
        public string Url { get; set; }
        
        public int Score { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime CreatedDateTime { get; set; }
        
    }
}
