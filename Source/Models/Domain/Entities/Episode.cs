namespace TDP.Models.Domain;

public class Episode : Movie
{
    public Episode(Guid id) : base(id)
    {
        Type = MovieTypes.EpisodeType;
    }

    public int Number { get; private set; }

    public int Season { get; private set; }

    public Series Series { get; private set; } = null!;

    public void SetNumber(int number)
    {
        this.Number = number;
    }

    public void SetSeason(int season)
    {
        this.Season = season;
    }

    public void SetSeries(Series series)
    {
        this.Series = series;
    }

}