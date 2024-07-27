using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using TDP.Models.Application.DataTransfer;
using TDP.Models.Domain;
using TDP.Models.Persistence;

namespace TDP.Models.Application
{
    public class MovieService : IMovieService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWorkManager uowManager;
        private readonly IRepository<Domain.Movie> movieRepository;
        private readonly IRepository<User> userRepository;
        private readonly ILogger<IMovieService> logger;

        public MovieService(
            IRepository<Domain.Movie> movieRepository,
            IRepository<User> userRepository,
            IUnitOfWorkManager uowManager,
            IMapper mapper,
            ILogger<IMovieService> logger)
        {
            this.mapper = mapper;
            this.movieRepository = movieRepository;
            this.userRepository = userRepository;
            this.uowManager = uowManager;
            this.logger = logger;
        }

            public Task<Domain.Movie?> GetMovie(string imdbId)
        {
            return this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification());
        }
        
        public Task SaveMovie(MovieDTO movie)
        {
            FormatMovie(movie);
            
            uowManager.BeginUnitOfWork();
            var dbmovie = new Domain.Movie(Guid.NewGuid());
            if (movie.Title is not null)
            {
                dbmovie.SetTitle(movie.Title);
            }

            if (movie.Plot is not null)
            {
                dbmovie.SetPlot(movie.Plot);
            }

            if (movie.imdbID is not null)
            {
                dbmovie.SetImdbId(movie.imdbID);
            }
            
            if (movie.Runtime != "N/A" && movie.Runtime is not null)
            {
                string runtime = Regex.Replace(movie.Runtime, "[A-Za-z ]", "");

                dbmovie.SetRuntime(long.Parse(runtime));
            }
            
            if (movie.Released != "N/A" && movie.Released is not null)
            {
                dbmovie.SetReleased(DateOnly.Parse(movie.Released));
            }

            if (movie.Country is not null)
            {
                dbmovie.SetCountry(movie.Country);
            }
            
            if (movie.imdbRating != "N/A")
            {
                dbmovie.SetImdbRating(Convert.ToDecimal(movie.imdbRating, new CultureInfo("en-US")));
            }

            if (movie.Poster is not null)
            {
                dbmovie.SetPosterUrl(movie.Poster);
            }
            
            var actorsNames = movie?.Actors?.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList() ?? [];
            var directorsNames = movie?.Director?.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList() ?? [];
            var writersNames = movie?.Writer?.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList() ?? [];
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
            logger.LogInformation($"Movie with id: {dbmovie.Id}, saved successfully.");
            return uowManager.Complete();

        }
        
        public async Task AddToWatchListAsync(string imdbId, Guid userId)
        {

            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null) 
            {
                logger.LogInformation($"When adding to watchlist, movie with imdbId: {imdbId} not found");
                throw new MovieNotFoundException($"Movie not found.");
            } 
            var user = await this.userRepository.FindByIdAsync(userId);
            if (user is null)
            {
                logger.LogInformation($"When adding to watchlist, user with userId: {userId} not found");
                throw new MovieNotFoundException($"User not found.");
            }
            movie.AddFollower(user);
        }

        public async Task<bool> AddedToWatchList(string imdbId, Guid userId)
        {
            var movie = (await this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()));
            return movie is not null && movie.HasFollower(userId);
        }
        
        public async Task RemoveFromWatchListAsync(string imdbId, Guid userId)
        {
            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null)
            {
                logger.LogInformation($"When removing from watchlist, movie with imdbId: {imdbId} not found");
                throw new MovieNotFoundException($"Movie not found.");
            }
            
            var user = await this.userRepository.FindByIdOrThrowAsync(userId);
            movie.RemoveFollower(user);
        }
        
        public async Task<MovieCollection> GetAllFromWatchList(Guid userId)
        {
            var user = await this.userRepository.FindByIdOrThrowAsync(userId, new UserIncludeSpecification());
            if (user is null)
            {
                logger.LogInformation($"When getting watchlist, user with userId: {userId} not found");
                throw new MovieNotFoundException($"User not found.");
            }
            MovieCollection aMovieCollection = new MovieCollection();
            List<MovieDTO> movieDtos = new List<MovieDTO>();
            foreach (var mov in user.FollowedMovies)
            {
                var movDto = mapper.Map<MovieDTO>(mov);
                movieDtos.Add(movDto);
            }
            aMovieCollection.Movies = movieDtos;
            aMovieCollection.TotalResults = movieDtos.Count.ToString();
            return aMovieCollection;
        }
        
        public async Task AddMovieRating(string imdbId, Guid userId, int rating, string? comment)
        {
            if (rating < 0 || rating > 5)
            {
                logger.LogInformation($"When adding movie rating, rating must be between 0 and 5");
                throw new MovieNotFoundException($"Rating must be between 0 and 5.");
            }
            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null)
            {
                logger.LogInformation($"When adding movie rating, movie with imdbId: {imdbId} not found");
                throw new MovieNotFoundException($"Could not rate the movie. Error logged.");
            }
            var user = await this.userRepository.FindByIdOrThrowAsync(userId);
            user.Rate(movie, rating, comment);
        }
        
        public async Task RemoveMovieRating(string imdbId, Guid userId)
        {
            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null)
            {
                logger.LogInformation($"When removing movie rating, movie with imdbId: {imdbId} not found");
                throw new MovieNotFoundException($"Movie not found.");
            }
            var user = await this.userRepository.FindByIdOrThrowAsync(userId);
            user.DeleteRating(movie);
        }
        
        public async Task<UserRating?> GetMovieRating(string imdbId, Guid userId)
        {
            var movie = (await this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()));
            if (movie is null)
            {
                return null;
            }
            var userRating = movie.Ratings.FirstOrDefault(r => r.UserId == userId);
            if (userRating is null)
            {
                return null;
            }
            return new UserRating
            {
                Rating = userRating.Rating,
                Comment = userRating.Comment
            };
        }
        
        public async Task SaveSeries(SeriesDTO series)
        {
            FormatMovie(series);
            
            var dbmovie = new Domain.Series(Guid.NewGuid());

            if (series.Title is not null)
            {
                dbmovie.SetTitle(series.Title);
            }

            if (series.Plot is not null)
            {
                dbmovie.SetPlot(series.Plot);
            }

            if (series.imdbID is not null)
            {
                dbmovie.SetImdbId(series.imdbID);
            }

            if (series.totalSeasons != "N/A" && series.totalSeasons is not null)
            {
                dbmovie.SetSeasons(int.Parse(series.totalSeasons));
            }

            if (series.Runtime != "N/A" && series.Runtime is not null)
            {
                string runtime = Regex.Replace(series.Runtime, "[A-Za-z ]", "");

                dbmovie.SetRuntime(long.Parse(runtime));
            }
            if (series.Released != "N/A" && series.Released is not null)
            {
                dbmovie.SetReleased(DateOnly.Parse(series.Released));
            }

            if (series.Country is not null)
            {
                dbmovie.SetCountry(series.Country);
            }
            if (series.imdbRating != "N/A")
            {
                dbmovie.SetImdbRating(Convert.ToDecimal(series.imdbRating, new CultureInfo("en-US")));
            }

            if (series.Poster is not null)
            {
                dbmovie.SetPosterUrl(series.Poster);
            }
            var actorsNames = series?.Actors?.Split([", "], StringSplitOptions.RemoveEmptyEntries).ToList() ?? [];
            var directorsNames = series?.Director?.Split([", "], StringSplitOptions.RemoveEmptyEntries).ToList() ?? [];
            var writersNames = series?.Writer?.Split([", "], StringSplitOptions.RemoveEmptyEntries).ToList() ?? [];
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
            logger.LogInformation("Series with id: {id}, saved successfully.", dbmovie.Id);
        }
        
        private static void FormatMovie(MovieDTO movie)
        {
            if (movie.Runtime != "N/A" && movie.Runtime is not null)
            {
                string runtime = Regex.Replace(movie.Runtime, "[A-Za-z ]", "");
                movie.SetRuntime(runtime);
            }
            if (movie.Released != "N/A" && movie.Released is not null)
            {
                movie.SetReleased(DateOnly.Parse(movie.Released).ToString());
            }
            if (movie.imdbRating != "N/A" && movie.imdbRating is not null)
            {
                movie.SetImdbRating(movie.imdbRating);
            }
            movie.Type = MovieTypes.MovieType;
            movie.SetIsAddedToWatchList(false);
        }
    }
}
