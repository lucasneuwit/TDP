using TDP.Models.Domain.Abstractions;

namespace TDP.Models.Domain;

public class User : BaseEntity
{
    public string Username { get; private set; } = null!;
 
    public string PasswordHash { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string EmailAddress { get; private set; } = null!;
    
    public DateOnly BirthDay { get; private set; }

    public bool IsAdministrator { get; set; } = false;

    public ISet<Movie> FollowedMovies { get; } = new SortedSet<Movie>();

    public ISet<UserRating> RatedMovies { get; } = new SortedSet<UserRating>();

    public void Follow(Movie movie)
    {
        FollowedMovies.Add(movie);
    }

    public void Unfollow(Movie movie)
    {
        FollowedMovies.Remove(movie);
    }

    public void Rate(Movie movie, int rating)
    {
        var userRating = new UserRating
        {
            MovieId = movie.Id,
            UserId = this.Id,
            Rating = rating
        };
        RatedMovies.Add(userRating);
    }
}