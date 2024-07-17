namespace TDP.Models.Application.DataTransfer;

/// <summary>
/// DataTransfer for making requests against Omdb API.
/// </summary>
public interface IRequest
{
    public string? ImdbId { get; set; }

    public string? Title { get; set; }

    public string? Type { get; set; }

    public string? ReleaseYear { get; set; }
}