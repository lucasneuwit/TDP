using Microsoft.EntityFrameworkCore;
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
            movies = await this._context.Set<Domain.Movie>().ToListAsync();
            return movies;
        }

        public void SaveMovie(MovieDTO movie)
        {
            var dbmovie = new Domain.Movie(Guid.NewGuid());
            dbmovie.SetTitle(movie.Title);
            dbmovie.SetPlot(movie.Plot);
            if (movie.Runtime != "N/A")
            {
                string runtime = Regex.Replace(movie.Runtime, "[A-Za-z ]", "");

                dbmovie.SetRuntime(long.Parse(runtime));
            }
            if (movie.Released != "N/A")
            {
                dbmovie.SetReleased(DateOnly.Parse(movie.Released));
            }
            dbmovie.SetCountry(movie.Country);
            if (movie.imdbRating != "N/A")
            {
                dbmovie.SetImdbRating(Convert.ToDecimal(movie.imdbRating, new CultureInfo("en-US")));
            }
            dbmovie.SetPosterUrl(movie.Poster);
            var actorsNames = movie.Actors.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var directorsNames = movie.Director.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var writersNames = movie.Writer.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var actor in actorsNames)
            {
                dbmovie.AddParticipant(actor, 0);
            }
            foreach (var director in directorsNames)
            {
                dbmovie.AddParticipant(director, 1);
            }
            foreach (var writer in writersNames)
            {
                dbmovie.AddParticipant(writer, 2);
            }
            _context.Add(dbmovie);
            _context.SaveChanges();
        }
    }
}
