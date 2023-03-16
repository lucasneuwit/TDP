namespace TDP.Models.Application.Services
{
    public interface IMovieService
    {
        Task<Domain.Movie> GetMovie(string title);

        Task <IEnumerable<Domain.Movie>> GetAllMovies(); 

        void SaveMovie(Movie movie);
    }
}
