using TDP.Models.Application.DataTransfer;
using TDP.Models.Domain;

namespace TDP.Models.Application
{
    public interface IMovieService
    {
        Task<Domain.Movie> GetMovie(string title);

        public void SaveSerie(SeriesDTO serie);
        public Task SaveMovie(MovieDTO movie);

        public void AddToWatchListAsync(string imdbId, Guid userId);

        public bool AddedToWatchList(string imdbId, Guid userId);

        public void RemoveFromWatchListAsync(string imdbId, Guid userId);

        public Task<MovieCollection> GetAllFromWatchList(Guid userId);

        public void AddMovieRating(string imdbId, Guid userId, int rating, string? comment);

        public void RemoveMovieRating(string imdbId, Guid userId);

        public UserRating GetMovieRating(string imdbId,Guid userId);
        public MovieDTO FormatMovie(MovieDTO movie);
    }
}
