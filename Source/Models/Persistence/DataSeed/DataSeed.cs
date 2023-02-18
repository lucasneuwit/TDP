using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class DataSeed
{
    public static Movie FirstMovie { get; set; } = new Movie()
    {
        Id = Guid.NewGuid(),
        Title = "Avengers",
        Plot = "Some not really important plot",
        Runtime = 250,
        Released = new DateOnly(2012, 1, 14),
        Country = "United States",
        ImdbRating = 9.3m,
        PosterUrl = ""
    };

    public static IEnumerable<Movie> MoviesToSeed { get; set; } = new List<Movie>()
    {
        FirstMovie
    };
}