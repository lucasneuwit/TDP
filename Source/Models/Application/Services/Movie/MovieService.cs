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

        private readonly IMapper _mapper;
        private readonly IUnitOfWorkManager uowManager;
        private readonly IRepository<Domain.Movie> movieRepository;
        private readonly IRepository<User> userRepository;
        private readonly ILogger<IMovieService> logger;

        public MovieService(IRepository<Domain.Movie> movieRepository, IRepository<User> userRepository, IUnitOfWorkManager uowManager, IMapper mapper, ILogger<IMovieService> logger)
        {
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
        // Summary:
        //     The method saves a movie in the database. Because there are differences in how 
        //     the data is stored in the DTO and the entity, the method converts the data from the DTO to the entity.
        //     
        // Params:
        //   MovieDto:
        //     Data transfer object of the movie entity.
        //
        // Returns:
        //     Returns a task that means the transaction has been completed.
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
            logger.LogInformation($"Movie with id: {dbmovie.Id}, saved successfully.");
            return uowManager.Complete();

        }

        // Summary:
        //     The method formats the data stored in the data transfer object.
        //     
        // Params:
        //   MovieDto:
        //     Data transfer object of the movie entity.
        //
        // Returns:
        //     Returns a formatted MovieDto.
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

        // Summary:
        //     The method adds a movie to the watchlist of a user.
        //     For this it makes a request to get both the Movie and the User.
        //     After that it adds the user as a follower to the movie object.
        //     In case either the movie or the user is not found it throws an exception.
        //     This exception is logged.
        //
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //   userId:
        //     userId of the user.
        //
        public void AddToWatchListAsync(string imdbId, Guid userId)
        {

            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null) 
            {
                logger.LogInformation($"When adding to watchlist, movie with imdbId: {imdbId} not found");
                throw new MovieNotFoundException($"Movie not found.");
            } 
            var user = this.userRepository.FindByUserIdAsync(userId).Result;
            if (user is null)
            {
                logger.LogInformation($"When adding to watchlist, user with userId: {userId} not found");
                throw new MovieNotFoundException($"User not found.");
            }
            movie.AddFollower(user);
        }

        // Summary:
        //     The method checks if a movie is in the watchlist of the current user.
        //     For this it checks the followers of the movies and compares the userId with the current user.
        //     This exception is logged.
        //
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //   userId:
        //     userId of the user.
        //
        //  Returns:
        //   It returns a boolean value that indicates if the movie is in the watchlist of the user.

        public bool AddedToWatchList(string imdbId, Guid userId)
        {
            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result.Followers.Any(follower => follower.Id == userId);
            return movie;
        }


        // Summary:
        //     The method removes a movie from the watchlist of a user.
        //     For this it makes a request to get both the Movie and the User.
        //     After that it removes the user as a follower to the movie object.
        //     In case either the movie or the user is not found it throws an exception.
        //     This exception is logged.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //   userId:
        //     userId of the user.
        public void RemoveFromWatchListAsync(string imdbId, Guid userId)
        {

            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null)
            {
                logger.LogInformation($"When removing from watchlist, movie with imdbId: {imdbId} not found");
                throw new MovieNotFoundException($"Movie not found.");
            }
            var user = this.userRepository.FindByUserIdAsync(userId).Result;
            if (user is null)
            {
                logger.LogInformation($"When removing from watchlist, user with userId: {userId} not found");
                throw new MovieNotFoundException($"User not found.");
            }
            movie.RemoveFollower(user);
        }

        // Summary:
        //     The method gets all the movies from the watchlist of a user.
        //     For this it makes a request to get the user.
        //     After that it loads the followedMovies from the user into a data transfer object.
        //     In case the user is not found it throws an exception.
        //     This exception is logged.
        //     
        // Params:
        //   userId:
        //     userId of the user.
        //
        //  Returns:
        //   It returns a collection of movies in the form of a data transfer object.
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
                var movDto = _mapper.Map<MovieDTO>(mov);
                movieDtos.Add(movDto);
            }
            aMovieCollection.Movies = movieDtos;
            aMovieCollection.TotalResults = movieDtos.Count.ToString();
            return aMovieCollection;
        }

        // Summary:
        //     The method adds a movie rating to a movie.
        //     For this it makes a request to get both the Movie and the User.
        //     After that it uses the rate method from the user.
        //     In case either the movie or the user is not found it throws an exception.
        //     This exception is logged.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //   userId:
        //     userId of the user.
        //   rating:
        //     rating inserted by the user.
        //   comment:
        //     comment inserted by the user.
        public void AddMovieRating(string imdbId, Guid userId, int rating, string? comment)
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
            var user = this.userRepository.FindByUserIdAsync(userId).Result;
            if (user is null)
            {
                logger.LogInformation($"When adding movie rating, user with userId: {userId} not found");
                throw new MovieNotFoundException($"Could not rate the movie. Error logged");
            }
            user.Rate(movie, rating, comment);
        }

        // Summary:
        //     The method removes the movie rating made by the current user.
        //     For this it makes a request to get both the Movie and the User.
        //     After that it uses delete rating method from the user.
        //     In case either the movie or the user is not found it throws an exception.
        //     This exception is logged.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //   userId:
        //     userId of the user.
        public void RemoveMovieRating(string imdbId, Guid userId)
        {
            var movie = this.movieRepository.FindByImdbId(imdbId, new MovieIncludeSpecification()).Result;
            if (movie is null)
            {
                logger.LogInformation($"When removing movie rating, movie with imdbId: {imdbId} not found");
                throw new MovieNotFoundException($"Movie not found.");
            }
            var user = this.userRepository.FindByUserIdAsync(userId).Result;
            if (user is null)
            {
                logger.LogInformation($"When removing movie rating, user with userId: {userId} not found");
                throw new MovieNotFoundException($"User not found.");
            }
            user.DeleteRating(movie);
        }

        // Summary:
        //     The method gets a movie rating from the user.
        //     For this it makes a request to get the user.
        //     After that it loads the ratings list and loads the one that matches the imdbId into an UserRating object.
        //     In case the user is not found it throws an exception.
        //     This exception is logged.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //   userId:
        //     userId of the user.
        //
        //  Returns:
        //   It returns an UserRating object that contains the rating and comment saved by the user.
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
                logger.LogInformation($"When getting movie rating, rating with userId: {userId} and imdbId {imdbId} not found");
                throw new MovieNotFoundException($"Rating not found.");
            }
        }

        // Summary:
        //     The method saves a series in the database. Because there are differences in how 
        //     the data is stored in the DTO and the entity, the method converts the data from the DTO to the entity.
        //     
        // Params:
        //   MovieDto:
        //     Data transfer object of the series entity.
        //
        // Returns:
        //     Returns a task that means the transaction has been completed.
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
            logger.LogInformation("Series with id: {id}, saved successfully.", dbmovie.Id);
        }
    }

}
