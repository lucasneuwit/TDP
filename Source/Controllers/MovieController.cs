using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TDP.Extensions;
using TDP.Models;
using TDP.Models.Application;
using TDP.Models.Application.DataTransfer;
using TDP.Models.Domain;
using TDP.Models.ViewModels;

namespace TDP.Controllers
{
    public class MovieController : Controller
    {
        private readonly IApiProvider _provider;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        
        public MovieController(IApiProvider provider, IMovieService service, IMapper mapper)
        {
            _provider = provider;
            _movieService = service;
            _mapper = mapper;
        }

        // Summary:
        //     The method first makes a request to the database for a movie. If the movie is not 
        //     found, it makes a request to the API provider. 
        //
        // Params:
        //   id:
        //     imdbId of the movie, provided by the API.
        //   type:
        //     whether the object is of type a movie, series or episode.   
        //   releaseYear:
        //     year the movie was released.
        //
        // Returns:
        //     A view that uses the MovieDto to show the movie details.
        //     In case of an error, it returns a view with an error message.    
        public async Task<IActionResult> FindById(string id, string? type, string? releaseYear)
        {
            
            try 
            {
                var movieDto = new MovieDTO();
                var res = await _movieService.GetMovie(id);
                if (res == null)
                {
                    IRequest aRequest = new Request(null, id, type, releaseYear);
                    var movie = _provider.FindAsync(aRequest).Result;
                    if (movie is SeriesDTO)
                    {
                        movieDto = _movieService.FormatMovie(movie);
                        _movieService.SaveSerie((SeriesDTO)movie);
                        
                    }
                    else
                    {
                        
                        movieDto = _movieService.FormatMovie(movie);
                        await _movieService.SaveMovie(movie);
                    }
                }
                else
                {
                    if (res is Series)
                    {
                        movieDto = _mapper.Map<SeriesDTO>(res);
                    }
                    else
                    {
                        movieDto = _mapper.Map<MovieDTO>(res);
                    }
                }
                return View("MovieDetail", movieDto);

            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError",new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
            

        }
        [HttpGet]


        // Summary:
        //     The method makes a request to the API for a list of movies that matches the parameters.
        //     
        // Params:
        //   title:
        //     title of the movie/episode/series to search for.
        //   type:
        //     whether the object is of type a movie, series or episode.
        //   releaseYear:
        //     year the movie was released.
        //   pageNumber:
        //     the number of the page to return. The API by default returns 10 movies per page.
        //
        // Returns:
        //     A view that shows the list of movies that match the search parameters.
        //     In case of an error, it returns a view with an error message.
        public async Task<IActionResult> Search(string title, string? type, string? releaseYear, int pageNumber)
        {
            try
            {
                IRequest aRequest = new Request(title, null, type, releaseYear);
                var res = await _provider.SearchAsync(aRequest, pageNumber);
                return View(res);
            }
            catch(HttpRequestException ex) 
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
            
        }
        
        // Summary:
        //     The method adds a movie to the watchlist of the current user.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //
        // Returns:
        //     A json that represents the result of the operation.
        //     In case the movie or user is not found, it returns a view with an error message.
        public async Task<IActionResult> AddToWatchlist(string imdbId)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                _movieService.AddToWatchListAsync(imdbId, currentUserId);
                return Json(currentUserId);
            }
            catch(MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }      
        }
        // Summary:
        //     The method checks if a movie is in the watchlist of the current user.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //
        // Returns:
        //     A boolean that represents if the movie is in the watchlist of the current user.
        public async Task<Boolean> AddedToWatchlist(string imdbId)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                bool isInWatchList = _movieService.AddedToWatchList(imdbId, currentUserId);
                return isInWatchList;
            }
            catch(ArgumentNullException ex)
            {
                return false;
            }
   
        }
        // Summary:
        //     The method removes a movie from the watchlist of the current user.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //
        // Returns:
        //     A json that represents the result of the operation.
        //     In case the movie or user is not found, it returns a view with an error message.
        public async Task<IActionResult> RemoveFromWatchlist(string imdbId)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                _movieService.RemoveFromWatchListAsync(imdbId, currentUserId);
                return Json(currentUserId);
            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
        }
        // Summary:
        //     The method makes a request to get all the movies from the watchlist of the current user.
        //     
        // Returns:
        //     A view that shows the list of movies in the watchlist of the current user.
        //     In case of an error, it returns a view with an error message.
        public async Task<IActionResult> GetWatchlist()
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                var res = await _movieService.GetAllFromWatchList(currentUserId);
                return View("Watchlist",res);
            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
            
        }
        // Summary:
        //     The method adds a rating to a movie.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //   rating:
        //     rating inserted by the user.
        //   comment:
        //     comment inserted by the user.
        //
        // Returns:
        //     A json that represents the result of the operation.
        //     In case the movie or user is not found, it returns a view with an error message.
        public async Task<IActionResult> RateMovie(string imdbId, int rating, string comment)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                _movieService.AddMovieRating(imdbId, currentUserId, rating, comment);
                return Json(new { success = true, message = "Movie rated successfully." });

            }
            catch (MovieNotFoundException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            
        }
        // Summary:
        //     The method removes a rating from a movie.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //
        // Returns:
        //     A json that represents the result of the operation.
        //     In case the movie or user is not found, it returns a view with an error message.
        public async Task<IActionResult> RemoveMovieRating(string imdbId)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                _movieService.RemoveMovieRating(imdbId, currentUserId);
                return Json(currentUserId);
            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
            
        }
        // Summary:
        //     The method returns the rating of a movie inserted by the current user.
        //     
        // Params:
        //   imdbId:
        //     imdbId of the movie.
        //
        // Returns:
        //     A json with the rating of the movie.
        public async Task<IActionResult> GetUserRating(string imdbId)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                UserRating rating = _movieService.GetMovieRating(imdbId, currentUserId);
                return Json(rating);
            }
            catch(MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
            
        }
    }
}
