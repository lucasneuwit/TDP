using TDP.Models.Domain.Enums;

namespace TDP.Models.Domain;

public class MovieParticipant
{
    public Movie Movie { get; set; } = null!;

    public Guid MovieId { get; set; }

    public string Name { get; set; } = null!;

    public ParticipantRole Role { get; private set; }

    public void SetMovie(Movie movie)
    {
        this.MovieId = movie.Id;
        this.Movie = movie;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public void SetRole(ParticipantRole role)
    {
        this.Role = role;
    }
}