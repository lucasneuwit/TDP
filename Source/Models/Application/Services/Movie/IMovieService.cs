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

        public Task AddToWatchListAsync(string imdbId, Guid userId);

        public bool AddedToWishList(string imdbId, Guid userId);

        public Task RemoveFromWatchListAsync(Guid movieId, Guid userId);

        public Task<MovieCollection> GetAllFromWatchList(Guid userId);

        public Task AddMovieRating(Guid movieId, Guid userId, int rating, string? comment);

        public Task RemoveMovieRating(Guid movieId, Guid userId, int rating, string? comment);

        public UserRating GetMovieRating(Guid movieId,Guid userId);
        public MovieDTO FormatMovie(MovieDTO movie);
    }
}
