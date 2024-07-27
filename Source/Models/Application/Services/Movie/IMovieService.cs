using TDP.Models.Application.DataTransfer;
using TDP.Models.Domain;

namespace TDP.Models.Application
{
    public interface IMovieService
    {
        /// <summary>
        /// Search for a movie by its title.
        /// </summary>
        /// <param name="title">The movie title.</param>
        /// <returns>The <see cref="Movie"/>, if found.</returns>
        Task<Domain.Movie?> GetMovie(string title);

        public Task SaveSeries(SeriesDTO series);
        
        /// <summary>
        /// Stores a <see cref="Movie"/> in the local storage.
        /// </summary>
        /// <param name="movie">The movie to be stored.</param>
        /// <returns></returns>
        public Task SaveMovie(MovieDTO movie);

        /// <summary>
        /// Adds a movie to the user's watchlist.
        /// </summary>
        /// <param name="imdbId">The movie imdb id.</param>
        /// <param name="userId">The user id.</param>
        public Task AddToWatchListAsync(string imdbId, Guid userId);

        /// <summary>
        /// Evaluates if a given movie is already present in the user's watchlist.
        /// </summary>
        /// <param name="imdbId">The movie imdb id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>A value indicating whether the movie is added to the user's watchlist.</returns>
        public Task<bool> AddedToWatchList(string imdbId, Guid userId);

        /// <summary>
        /// Attempts to remove a movie from a user's watchlist.
        /// </summary>
        /// <param name="imdbId">The movie imdb id.</param>
        /// <param name="userId">The user id.</param>
        public Task RemoveFromWatchListAsync(string imdbId, Guid userId);

        /// <summary>
        /// Gets a user watchlist.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The user watchlist.</returns>
        public Task<MovieCollection> GetAllFromWatchList(Guid userId);

        /// <summary>
        /// Adds a <see cref="UserRating"/> for a movie.
        /// </summary>
        /// <param name="imdbId">The movie imdb id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rating">The movie rating value.</param>
        /// <param name="comment">The rating comment, if any.</param>
        public Task AddMovieRating(string imdbId, Guid userId, int rating, string? comment);

        /// <summary>
        /// Removes a <see cref="UserRating"/> from a movie.
        /// </summary>
        /// <param name="imdbId">The movie imdb id.</param>
        /// <param name="userId">The user id.</param>
        public Task RemoveMovieRating(string imdbId, Guid userId);

        /// <summary>
        /// Gets a <see cref="UserRating"/> for a specific movie and user.
        /// </summary>
        /// <param name="imdbId">The movie imdb id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The <see cref="UserRating"/>, if found.</returns>
        public Task<UserRating?> GetMovieRating(string imdbId,Guid userId);
    }
}
