using TDP.Models.Domain.Enums;

namespace TDP.Models.Domain;

public class Series : Movie
{
    public Series(Guid id) : base(id) 
    {
        Type = MovieTypes.SeriesType;
    }
    
    public int Seasons { get; private set; }

    //public ICollection<Episode> Episodes { get; } = new List<Episode>();

    public void SetSeasons(int seasons)
    {
        this.Seasons = seasons;
    }

   /* public void AddEpisode(Episode episode)
    {
        if (this.Episodes.Any(e => e.Id == episode.Id))
        {
            return;
        }
        
        this.Episodes.Add(episode);
        episode.SetSeries(this);
    }*/
}