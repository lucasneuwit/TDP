using TDP.Models.Domain;

namespace TDP.Models.Persistence;

public class DataSeed
{
    public DataSeed()
    {
        Setup();
    }

    private Movie FirstMovie { get; set; } = null!;
    private Movie SecondMovie { get; set; } = null!;

    private User FirstUser { get; set; } = null!;

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
        FirstMovie.SetImdbId(string.Empty);
        SecondMovie = new Movie(Guid.NewGuid());
        SecondMovie.SetTitle("Batman");
        SecondMovie.SetPlot("Some not really important plot");
        SecondMovie.SetRuntime(220);
        SecondMovie.SetReleased(new DateOnly(2013, 1, 14));
        SecondMovie.SetCountry("Somalia");
        SecondMovie.SetImdbRating(8.3m);
        SecondMovie.SetPosterUrl(string.Empty);
        SecondMovie.SetImdbId(string.Empty);

        MoviesToSeed = new List<Movie>()
        {
            FirstMovie,SecondMovie
        };

        FirstUser = new User(Guid.NewGuid());
        FirstUser.SetName("Yusty");
        FirstUser.SetUsername("elyusty");
        FirstUser.SetPassword("boca");
        FirstUser.SetLastname("Fabra");
        FirstUser.SetEmailAddress("elYusty@bokita.com");
        FirstUser.SetBirthday(new DateOnly(1945, 1, 1));

        UsersToSeed = new List<User>()
        {
            FirstUser
        };

    }

    public IEnumerable<Movie> MoviesToSeed { get; private set; } = new List<Movie>();

    public IEnumerable<User> UsersToSeed { get; private set; } = new List<User>();
}