using System.Text.Json.Serialization;

namespace TDP.Models.Application;

public record Movie
{
    public Guid Id { get; set; }
    public string? imdbId { get; set; }

    public string? Title { get; set; }
    public string? Year { get; set; }

    public string? Plot { get; set; }

    public string? Runtime { get; set; }

    public string? Type { get; set; }

    public string? Released { get; set; }

    public string? Poster { get; set; }

    public string? Country { get; set; }

    public string? imdbRating { get; set; }
}