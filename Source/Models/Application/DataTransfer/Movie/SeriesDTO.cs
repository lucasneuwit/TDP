namespace TDP.Models.Application.DataTransfer;

public record SeriesDTO : MovieDTO
{
    public string? totalSeasons { get; set; } 

}