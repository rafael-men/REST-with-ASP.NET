using System.Text.Json.Serialization;
using main.Hypermedia;
using main.Hypermedia.Abstract;

namespace main.VO
{
    public class PersonVO : ISupportHypermedia
    {
        [JsonPropertyName("id")]
        public long Id {  get; set; }
        [JsonPropertyName("name")]
        public string FirstName { get; set; }
        [JsonPropertyName("surname")]
        public string LastName { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        public bool Enabled { get; set; }

        public List<MyHypermediaLink> Links { get; set; } = new List<MyHypermediaLink>();
    }
}
