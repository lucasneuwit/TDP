using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using TDP.Models.Application.DataTransfer;
using TDP.Models.Application.Services.Movie;
using TDP.Models.Domain;
using TDP.Models.Domain.Enums;
using TDP.Models.Persistence;
using TDP.Models.Persistence.Extensions;
using TDP.Models.Persistence.UnitOfWork;

namespace TDP.Models.Application.Services
{
    public class MovieService : IMovieService
    {

        private readonly TdpDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkManager uowManager;
        private readonly IRepository<Domain.Movie> movieRepository;
        private readonly IRepository<User> userRepository;

        public MovieService(IRepository<Domain.Movie> repository, IUnitOfWorkManager uowManager,TdpDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.movieRepository = repository;
            this.uowManager = uowManager;
        }

        // TODO: agregar excepcion cuando viene vacio
        public async Task<Domain.Movie> GetMovie(string imdbId)
        {
            var movie = await this.movieRepository.FindByImdbId(imdbId);
                return movie;

        }

        public async Task<IEnumerable<Domain.Movie>> GetAllMovies()
        {

            var movies = await this.movieRepository.AllAsync();
            if (movies is not null)
            {
                return movies;
            }
            else
            {
                throw new MovieNotFoundException($"List of movies not found.");
            }
        }

        public async Task SaveMovie(MovieDTO movie)
        {
            var dbmovie = new Domain.Movie(Guid.NewGuid());
            dbmovie.SetTitle(movie.Title);
            dbmovie.SetPlot(movie.Plot);
            dbmovie.SetImdbId(movie.imdbID);
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
            await this.movieRepository.CreateAsync(dbmovie);
            //_context.Add(dbmovie);
            _context.SaveChanges();
        }

        public MovieDTO FormatMovie(MovieDTO movie)
        {
            if (movie.Runtime != "N/A")
            {
                string runtime = Regex.Replace(movie.Runtime, "[A-Za-z ]", "");
                movie.SetRuntime(runtime);
            }
            if (movie.Released != "N/A")
            {
                movie.SetReleased(DateOnly.Parse(movie.Released).ToString());
            }
            if (movie.imdbRating != "N/A")
            {
                movie.SetImdbRating(movie.imdbRating);
            }
            movie.Type = MovieTypes.MovieType;
            movie.SetIsAddedToWatchList(false);
            return movie;
        }

        public void AddToWatchListAsync(string imdbId, string username)
        {

            var movie = this.movieRepository.FindByImdbId(imdbId).Result;
            //var movie = _context.Set<Domain.Movie>().First(mov => mov.ImdbId.Equals(imdbId));
            //var user = _context.Set<Domain.User>().First(usr => usr.Id.Equals(userId));
            var user = userRepository.FindByUsernameAsync(username).Result;
            
            movie.AddFollower(user);
            //return _context.SaveChangesAsync();

        }
        public bool AddedToWishList(string imdbId, Guid userId)
        {
            //var movie = repository.FindByImdbId(imdbId);
            var movie = _context.Set<Domain.Movie>().Include(m => m.Followers).First(mov => mov.ImdbId.Equals(imdbId));
            return movie.Followers.Any(follower => follower.Id == userId);
        }

        public Task RemoveFromWatchListAsync(Guid movieId, Guid userId)
        {
            var user = _context.Set<Domain.User>().First(usr => usr.Id.Equals(userId));
            var movie = _context.Set<Domain.Movie>().First(mov => mov.Id.Equals(movieId));
            movie.RemoveFollower(user);
            return _context.SaveChangesAsync();
        }

        public async Task<MovieCollection> GetAllFromWatchList(Guid userId)
        {
            var user = _context.Set<Domain.User>().Include(m => m.FollowedMovies).First(usr => usr.Id.Equals(userId));
            MovieCollection aMovieCollection = new MovieCollection();
            List<MovieDTO> movieDtos = new List<MovieDTO>();
            foreach (var mov in user.FollowedMovies)
            {
                var movDto = _mapper.Map<MovieDTO>(mov);
                movieDtos.Add(movDto);
            }
            aMovieCollection.Movies = movieDtos;
            aMovieCollection.TotalResults = movieDtos.Count.ToString();
            return aMovieCollection;
        }

        Task IMovieService.AddMovieRating(Guid movieId, Guid userId, int rating, string? comment)
        {
            var movie = _context.Set<Domain.Movie>().First(mov => mov.Id.Equals(movieId));
            var user = _context.Set<Domain.User>().First(usr => usr.Id.Equals(userId));
            user.Rate(movie, rating, comment);
            return _context.SaveChangesAsync();
        }

        Task IMovieService.RemoveMovieRating(Guid movieId, Guid userId, int rating, string? comment)
        {
            var movie = _context.Set<Domain.Movie>().First(mov => mov.Id.Equals(movieId));
            var user = _context.Set<Domain.User>().First(usr => usr.Id.Equals(userId));
            user.DeleteRating(movie);
            return _context.SaveChangesAsync();
        }

        UserRating IMovieService.GetMovieRating(Guid movieId, Guid userId)
        {
            var userRating = _context.Set<Domain.User>()
        .Where(u => u.Id == userId)
        .SelectMany(u => u.RatedMovies)
        .FirstOrDefault(r => r.MovieId == movieId);

            if (userRating != null)
            {

                return new UserRating
                {
                    Rating = userRating.Rating,
                    Comment = userRating.Comment
                };
            }

            return null;
        }

        void IMovieService.SaveSerie(SeriesDTO serie)
        {
            var dbmovie = new Domain.Series(Guid.NewGuid());
            dbmovie.SetTitle(serie.Title);
            dbmovie.SetPlot(serie.Plot);
            dbmovie.SetImdbId(serie.imdbID);

            if (serie.totalSeasons != "N/A")
            {
                dbmovie.SetSeasons(int.Parse(serie.totalSeasons));
            }

            if (serie.Runtime != "N/A")
            {
                string runtime = Regex.Replace(serie.Runtime, "[A-Za-z ]", "");

                dbmovie.SetRuntime(long.Parse(runtime));
            }
            if (serie.Released != "N/A")
            {
                dbmovie.SetReleased(DateOnly.Parse(serie.Released));
            }
            dbmovie.SetCountry(serie.Country);
            if (serie.imdbRating != "N/A")
            {
                dbmovie.SetImdbRating(Convert.ToDecimal(serie.imdbRating, new CultureInfo("en-US")));
            }
            dbmovie.SetPosterUrl(serie.Poster);
            var actorsNames = serie.Actors.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var directorsNames = serie.Director.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var writersNames = serie.Writer.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
