using System.Runtime.CompilerServices;
using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class DataSeed
{
    public DataSeed()
    {
        Setup();
    }

    private Movie FirstMovie { get; set; } = null!;

    private void Setup()
    {
        FirstMovie = new Movie(Guid.NewGuid());
        FirstMovie.SetTitle("Avengers");
        FirstMovie.SetPlot("Some not really important plot");
        FirstMovie.SetRuntime(250);
        FirstMovie.SetReleased(new DateOnly(2012, 1, 14));
        FirstMovie.SetCountry("United States");
        FirstMovie.SetImdbRating(9.3m);
        FirstMovie.SetPosterUrl(string.Empty);

        MoviesToSeed = new List<Movie>()
        {
            FirstMovie,
        };

    }

    public IEnumerable<Movie> MoviesToSeed { get; private set; } = new List<Movie>();
}