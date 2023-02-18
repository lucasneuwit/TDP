namespace TDP.Models.Domain;

public record UserRating
{
    public Guid UserId { get; set; }

    public Guid MovieId { get; set; }

    public int Rating { get; set; }
}