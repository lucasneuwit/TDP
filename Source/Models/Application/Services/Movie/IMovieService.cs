using TDP.Models.Application.DataTransfer;
using TDP.Models.Domain;

namespace TDP.Models.Application.Services
{
    public interface IMovieService
    {
        Task<Domain.Movie> GetMovie(string title);

        Task <IEnumerable<Domain.Movie>> GetAllMovies();

        public void SaveSerie(SeriesDTO serie);
        public Task SaveMovie(MovieDTO movie);

        public void AddToWatchListAsync(string imdbId, Guid userId);

        public bool AddedToWishList(string imdbId, Guid userId);

        public void RemoveFromWatchListAsync(string imdbId, Guid userId);

        public Task<MovieCollection> GetAllFromWatchList(Guid userId);

        public void AddMovieRating(string imdbId, Guid userId, int rating, string? comment);

        public void RemoveMovieRating(string imdbId, Guid userId);

        public UserRating GetMovieRating(string imdbId,Guid userId);
        public MovieDTO FormatMovie(MovieDTO movie);
    }
}
