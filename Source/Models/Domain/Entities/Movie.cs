﻿namespace TDP.Models.Domain;

public class Movie : BaseEntity
{
    public Movie(Guid id) : base(id)
    {
        Type = MovieTypes.MovieType;
    }
    public string ImdbId { get; set; } = null!;
    public string Title { get; private set; } = null!;

    public string Plot { get; private set; } = null!;

    public long Runtime { get; private set; }
    
    public string Type { get; protected init; }
    
    public DateOnly Released { get; private set; }

    public string PosterUrl { get; private set; } = null!;

    public string Country { get; private set; } = null!;

    public decimal ImdbRating { get; private set; }

    public ICollection<MovieParticipant> Participants { get; } = new List<MovieParticipant>();

    public ICollection<UserRating> Ratings { get; } = new List<UserRating>();

    public ICollection<User> Followers { get; } = new List<User>();

    public void SetTitle(string title)
    {
        this.Title = title;
    }
    public void SetImdbId(string imdbId)
    {
        this.ImdbId = imdbId;
    }

    public void SetPlot(string plot)
    {
        this.Plot = plot;
    }

    public void SetRuntime(long runtime)
    {
        this.Runtime = runtime;
    }
    
    public void SetReleased(DateOnly releaseDate)
    {
        this.Released = releaseDate;
    }

    public void SetPosterUrl(string posterUrl)
    {
        this.PosterUrl = posterUrl;
    }

    public void SetCountry(string country)
    {
        this.Country = country;
    }

    public void SetImdbRating(decimal imdbRating)
    {
        this.ImdbRating = imdbRating;
    }

    public void AddParticipant(string name, int role)
    {
        var movieParticipant = new MovieParticipant();
        if (this.Participants.Any(e => e.Name == movieParticipant.Name && e.Role == movieParticipant.Role))
        {
            return;
        }
        
        movieParticipant.SetName(name);
        movieParticipant.SetRole((ParticipantRole)role);
        movieParticipant.SetMovie(this);
        this.Participants.Add(movieParticipant);
    }

    public void AddFollower(User user)
    {
        if (Followers.All(e => e.Id != user.Id))
        {
            user.Follow(this);
            Followers.Add(user);
        }
    }

    public void RemoveFollower(User user)
    {
        user.Unfollow(this);
        Followers.Remove(user);
    }

    public void AddUserRating(UserRating userRating)
    {
        if (this.Ratings.SingleOrDefault(e => e.UserId == userRating.UserId) is { } currentRating)
        {
            this.Ratings.Remove(currentRating);
        }
        
        this.Ratings.Add(userRating);
    }

    public bool HasFollower(Guid userId)
    {
        return this.Followers.Any(e => e.Id == userId);
    }
    
}