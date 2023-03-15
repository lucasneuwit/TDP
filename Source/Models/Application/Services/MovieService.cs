using TDP.Models.Persistence;

namespace TDP.Models.Application.Services
{
    public class MovieService : IMovieService
    {
        
        private readonly TdpDbContext _context;

        public MovieService(TdpDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Movie> GetMovie(string title)
        {
            var movie = _context.Set<Domain.Movie>().Where(mov => mov.Title.Equals(title)).First();
            return movie;
        }

        public async Task<IEnumerable<Domain.Movie>> GetAllMovies()
        {
            List<Domain.Movie> movies;
            movies = this._context.Set<Domain.Movie>().ToList();
            return movies;


        }
    }
}
