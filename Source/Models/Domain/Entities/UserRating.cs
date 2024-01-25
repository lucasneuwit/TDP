namespace TDP.Models.Domain;

public record UserRating
{
    public User User { get; private set; } = null!;

    public Guid UserId { get; private set; }

    public Movie Movie { get; private set; } = null!;

    public Guid MovieId { get; private set; }

    public int Rating { get; set; }

    public string? Comment { get; set; } 

    public void SetRating(int rating)
    {
        this.Rating = rating;
    }

    public void SetUser(User user)
    {
        this.User = user;
        this.UserId = user.Id;
    }

    public void SetMovie(Movie movie)
    {
        this.Movie = movie;
        this.MovieId = movie.Id;
    }

    public void SetComment (string comment) 
    {
        this.Comment = comment;
    }
}