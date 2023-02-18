namespace TDP.Models.Domain;

public class MovieParticipant
{
    public Guid MovieId { get; set; }

    public string Name { get; set; }

    public string Role { get; set; }
}