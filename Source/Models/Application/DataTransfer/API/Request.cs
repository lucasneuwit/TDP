namespace TDP.Models.Application
{
    public class Request : IRequest
    {
        public string? ImdbId { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? ReleaseYear { get; set; }
    }
}
