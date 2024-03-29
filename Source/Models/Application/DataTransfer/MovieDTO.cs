﻿namespace TDP.Models.Application;

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

}