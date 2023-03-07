using System.Text.Json.Serialization;

namespace TDP.Models.Application;

public record Movie
{
    [JsonPropertyName("imdbID")]
    public string? Id { get; set; }

    public string? Title { get; set; }
    public string? Year { get; set; }

    public string? Plot { get; set; }

    public string? Runtime { get; set; }

    public string? Type { get; set; }

    public string? Released { get; set; }
    [JsonPropertyName("Poster")]
    public string? PosterUrl { get; set; }

    public string? Country { get; set; }
    [JsonPropertyName("imdbRating")]
    public string? ImdbRating { get; set; }
}