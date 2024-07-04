using TDP.Models.Domain.Abstractions;

namespace TDP.Models.Domain;

public class User : BaseEntity
{
    public User(Guid id) : base(id)
    {
    }
    
    public string Username { get; private set; } = null!;
 
    public string PasswordHash { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string EmailAddress { get; private set; } = null!;
    
    public DateOnly BirthDay { get; private set; }

    public bool IsAdministrator { get; set; } = false;

    public ICollection<Movie> FollowedMovies { get; } = new List<Movie>();

    public ICollection<UserRating> RatedMovies { get; } = new List<UserRating>();

    public byte[]? ProfilePicture { get; set; }

    public void SetUsername(string username)
    {
        this.Username = username;
    }

    public void SetPassword(string password)
    {
        this.PasswordHash = password;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public void SetLastname(string lastname)
    {
        this.LastName = lastname;
    }

    public void SetEmailAddress(string emailAddress)
    {
        this.EmailAddress = emailAddress;
    }

    public void SetBirthday(DateOnly birthday)
    {
        this.BirthDay = birthday;
    }

    public void SetProfilePicture(byte[] profilePicture)
    {
        
        this.ProfilePicture = profilePicture;
    }
    
    public void Follow(Movie movie)
    {
        if (FollowedMovies.All(e => e.Id != movie.Id))
        {
            FollowedMovies.Add(movie);
        }
    }

    public void Unfollow(Movie movie)
    {
        FollowedMovies.Remove(movie);
    }

    public void Rate(Movie movie, int rating, string? comment)
    {
        var newUserRating = new UserRating();
        newUserRating.SetRating(rating);
        newUserRating.SetComment(comment);
        newUserRating.SetMovie(movie);
        newUserRating.SetUser(this);
        
        // Remove existent rating for this movie and user if any.
        if (RatedMovies.SingleOrDefault(e => e.MovieId == movie.Id) is { } userRating)
        {
            RatedMovies.Remove(userRating);
        }

        RatedMovies.Add(newUserRating);
        movie.AddUserRating(newUserRating);
    }

    public void DeleteRating(Movie movie)
    {
        // Find and remove the rating for the specified movie.
        var userRating = RatedMovies.SingleOrDefault(e => e.MovieId == movie.Id);

        if (userRating != null)
        {
            RatedMovies.Remove(userRating);
        }
    }

}