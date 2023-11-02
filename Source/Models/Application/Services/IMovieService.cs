namespace TDP.Models.Application.Services
{
    public interface IMovieService
    {
        Task<Domain.Movie> GetMovie(string title);

        Task <IEnumerable<Domain.Movie>> GetAllMovies(); 

       public void SaveMovie(MovieDTO movie);

        public Task AddToWatchListAsync(Guid movieId, Guid userId);

    }
}
