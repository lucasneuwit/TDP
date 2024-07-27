using System.Text.Json.Serialization;

namespace TDP.Models.Application.DataTransfer
{
    public class MovieCollection
    {
        [JsonPropertyName("Search")] 
        public IEnumerable<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
        
        [JsonPropertyName("totalResults")]
        public string TotalResults { get; set; }
        
        public string Response { get; set; }
        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string Type { get; set; }

        public string Released { get; set; }
    }
}
