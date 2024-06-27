using TDP.Models.Domain.Enums;

namespace TDP.Models.Application;

public record MovieDTO
{
    public Guid Id { get; set; }
    public string? imdbID { get; set; }

    public string? Title { get; set; }
    public string? Year { get; set; }

    public string? Plot { get; set; }

    public string? Runtime { get; set; }

    public string? Type { get; set; }

    public string? Released { get; set; }

    public string? Poster { get; set; }

    public string? Country { get; set; }

    public string? Genre { get; set; }

    public string? imdbRating { get; set; }

    public string? Director { get; set; }
    public string? Writer { get; set; }

    public string? Actors { get; set; }

    public bool? IsAddedToWatchList { get; set; }

    public bool? IsRatedByUser { get; set; }

    public void SetTitle(string title)
    {
        this.Title = title;
    }
    public void SetImdbId(string imdbId)
    {
        this.imdbID = imdbId;
    }

    public void SetPlot(string plot)
    {
        this.Plot = plot;
    }

    public void SetRuntime(string runtime)
    {
        this.Runtime = runtime;
    }

    public void SetReleased(string releaseDate)
    {
        this.Released = releaseDate;
    }

    public void SetPosterUrl(string posterUrl)
    {
        this.Poster = posterUrl;
    }

    public void SetCountry(string country)
    {
        this.Country = country;
    }

    public void SetImdbRating(string imdbRating)
    {
        this.imdbRating = imdbRating;
    }
    public void SetIsAddedToWatchList (bool boolean)
    {
        this.IsAddedToWatchList = boolean;
    }
}