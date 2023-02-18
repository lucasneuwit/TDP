namespace TDP.Models.Application;

public record Movie
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Plot { get; set; }

    public string? Runtime { get; set; }

    public string? Type { get; set; }

    public string? Released { get; set; }

    public string? PosterUrl { get; set; }

    public string? Country { get; set; }

    public string? ImdbRating { get; set; }
}