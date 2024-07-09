using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using TDP.Models.Application.DataTransfer;
using TDP.Models.Application.Services.Movie;
using TDP.Models.Domain;
using TDP.Models.Domain.Enums;
using TDP.Models.Persistence;
using TDP.Models.Persistence.Extensions;
using TDP.Models.Persistence.Specifications;
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
        private readonly ILogger<IMovieService> logger;

        public MovieService(IRepository<Domain.Movie> movieRepository, IRepository<User> userRepository, IUnitOfWorkManager uowManager,TdpDbContext context, IMapper mapper, ILogger<IMovieService> logger)
        {
            _context = context;
            _mapper = mapper;
            this.movieRepository = movieRepository;
            this.userRepository = userRepository;
            this.uowManager = uowManager;
            this.logger = logger;
        }

            public async Task<Domain.Movie> GetMovie(string imdbId)
        {
            var movie = await this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification());
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

        public Task SaveMovie(MovieDTO movie)
        {
            uowManager.BeginUnitOfWork();
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
            movieRepository.CreateAsync(dbmovie);

            return uowManager.Complete();

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

        public void AddToWatchListAsync(string imdbId, Guid userId)
        {

            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null) 
            {
                throw new MovieNotFoundException($"Movie not found.");
            } 
            var user = this.userRepository.FindByUserIdAsync(userId).Result;
            if (user is null)
            {
                throw new MovieNotFoundException($"User not found.");
            }
            movie.AddFollower(user);
        }
        public bool AddedToWishList(string imdbId, Guid userId)
        {
            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result.Followers.Any(follower => follower.Id == userId);
            return movie;
        }
            
        

        public void RemoveFromWatchListAsync(string imdbId, Guid userId)
        {

            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null)
            {
                throw new MovieNotFoundException($"Movie not found.");
            }
            var user = this.userRepository.FindByUserIdAsync(userId).Result;
            if (user is null)
            {
                throw new MovieNotFoundException($"User not found.");
            }
            movie.RemoveFollower(user);
        }

        public async Task<MovieCollection> GetAllFromWatchList(Guid userId)
        {
            //tiene include
            var user = await this.userRepository.FindByIdOrThrowAsync(userId, new UserIncludeSpecification());
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
        public void AddMovieRating(string imdbId, Guid userId, int rating, string? comment)
        {
            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null)
            {
                throw new MovieNotFoundException($"Movie not found.");
            }
            var user = this.userRepository.FindByUserIdAsync(userId).Result;
            if (user is null)
            {
                throw new MovieNotFoundException($"User not found.");
            }
            user.Rate(movie, rating, comment);
        }

        public void RemoveMovieRating(string imdbId, Guid userId)
        {
            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null)
            {
                throw new MovieNotFoundException($"Movie not found.");
            }
            var user = this.userRepository.FindByUserIdAsync(userId).Result;
            if (user is null)
            {
                throw new MovieNotFoundException($"User not found.");
            }
            user.DeleteRating(movie);
        }

        UserRating IMovieService.GetMovieRating(string imdbId, Guid userId)
        {
            var userRating = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result.Ratings.FirstOrDefault(r => r.UserId == userId);
            if (userRating != null)
            {

                return new UserRating
                {
                    Rating = userRating.Rating,
                    Comment = userRating.Comment
                };
            }
            else
            {
                throw new MovieNotFoundException($"Rating not found.");
            }
        }

        async void IMovieService.SaveSerie(SeriesDTO serie)
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
            await this.movieRepository.CreateAsync(dbmovie);
        }
    }

}
