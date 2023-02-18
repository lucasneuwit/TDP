using TDP.Models.Domain.Enums;

namespace TDP.Models.Domain;

public class Episode : Movie
{
    public Episode()
    {
        Type = MovieTypes.EpisodeType;
    }
    
    public int Number { get; set; }
    
    public int Season { get; set; }

    public Series Series { get; set; }
}