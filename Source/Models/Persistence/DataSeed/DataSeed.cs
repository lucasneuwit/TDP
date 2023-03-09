using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class DataSeed
{
    public DataSeed()
    {
        Setup();
    }

    private static Movie FirstMovie { get; set; } = null!;

    private static void Setup()
    {
        FirstMovie = new Movie(Guid.NewGuid());
        FirstMovie.SetTitle("Avengers");
        FirstMovie.SetPlot("Some not really important plot");
        FirstMovie.SetRuntime(250);
        FirstMovie.SetReleased(new DateOnly(2012, 1, 14));
        FirstMovie.SetCountry("United States");
        FirstMovie.SetImdbRating(9.3m);
    }

    public static IEnumerable<Movie> MoviesToSeed { get; } = new List<Movie>()
    {
        FirstMovie
    };
}