namespace TDP.Models.Application;

public record SeriesDTO : MovieDTO
{
    public string? totalSeasons { get; set; } 

}