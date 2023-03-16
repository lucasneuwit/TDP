using System.Globalization;
using System.Text.RegularExpressions;
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
            var movie = _context.Set<Domain.Movie>().Where(mov => mov.Title.Equals(title)).FirstOrDefault();
            return movie;
        }

        public async Task<IEnumerable<Domain.Movie>> GetAllMovies()
        {
            List<Domain.Movie> movies;
            movies = this._context.Set<Domain.Movie>().ToList();
            return movies;

        }

        void IMovieService.SaveMovie(Movie movie)
        {
            var dbmovie = new Domain.Movie(Guid.NewGuid());
            dbmovie.SetTitle(movie.Title);
            dbmovie.SetPlot(movie.Plot);
            string runtime = Regex.Replace(movie.Runtime, "[A-Za-z ]", "");
            
            dbmovie.SetRuntime(long.Parse(runtime));
            dbmovie.SetReleased(DateOnly.Parse(movie.Released));
            dbmovie.SetCountry(movie.Country);
            if (movie.imdbRating != "N/A")
            {
                dbmovie.SetImdbRating(Convert.ToDecimal(movie.imdbRating, new CultureInfo("en-US")));

            }
            dbmovie.SetPosterUrl(movie.Poster);
            _context.Add(dbmovie);
            _context.SaveChanges();

        }
    }
}
