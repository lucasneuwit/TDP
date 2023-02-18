namespace TDP.Models.Application;

public record Series
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

    public int Seasons { get; set; }

    public ICollection<Episode> Episodes { get; set; }
}