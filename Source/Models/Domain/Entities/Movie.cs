using TDP.Models.Domain.Abstractions;
using TDP.Models.Domain.Enums;

namespace TDP.Models.Domain;

public class Movie : BaseEntity
{
    public Movie()
    {
        Type = MovieTypes.MovieType;
    }
    
    public string Title { get; set; }

    public string Plot { get; set; }

    public long Runtime { get; set; }
    
    public string Type { get; protected set; }
    
    public DateOnly Released { get; set; }

    public string PosterUrl { get; set; }
    
    public string Country { get; set; }

    public decimal ImdbRating { get; set; }

    public IEnumerable<MovieParticipant> Participants { get; set; }

    public ISet<UserRating> Ratings { get; set; }
}