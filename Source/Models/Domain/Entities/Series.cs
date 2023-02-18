using TDP.Models.Domain.Enums;

namespace TDP.Models.Domain;

public class Series : Movie
{
    public Series()
    {
        Type = MovieTypes.SeriesType;
    }
    
    public int Seasons { get; set; }

    public ICollection<Episode> Episodes { get; set; }
}