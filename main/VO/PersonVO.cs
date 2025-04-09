using System.Text.Json.Serialization;

namespace main.VO
{
    public class PersonVO
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
    }
}
