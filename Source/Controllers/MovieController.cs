using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TDP.Models;
using TDP.Models.Application;
using TDP.Models.Application.Services;
using TDP.Models.Application.Services.Movie;
using TDP.Models.Domain;

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


        // OK
        public async Task<IActionResult> FindByTitle(string title, string? type, string? releaseYear)
        {
            var res = await _movieService.GetMovie(title);
            if (res == null)
            {
                IRequest aRequest = new Request(title, null, type, releaseYear);
                var movie = await _provider.FindAsync(aRequest);
                _movieService.SaveMovie(movie);
                return View("MovieDetail",movie);
            }

            var movieDto = _mapper.Map<MovieDTO>(res);
            return View("MovieDetail",movieDto);
        }
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
        //OK
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
        public async Task<IActionResult> GetMovie(string imdbId)
        {
            try
            {
                var movie = await _movieService.GetMovie(imdbId);
                var movieDto = _mapper.Map<MovieDTO>(movie);
                return Json(movieDto);

            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
            
        }
        //OK
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                List<MovieDTO> movielist;
                var movies = await _movieService.GetAllMovies();
                movielist = _mapper.Map<List<MovieDTO>>(movies);
                return Json(movielist);
            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
        }
        //OK
        public async Task<IActionResult> AddToWishList(string imdbId, Guid userId, bool isInWatchList)
        {

            try
            {
                _movieService.AddToWatchListAsync(imdbId, userId);
                return Json(userId);
            }
            catch(MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }      
        }
        public async Task<Boolean> AddedToWishList(string imdbId, Guid userId)
        {
            bool isInWatchList = _movieService.AddedToWishList(imdbId, userId);
            return isInWatchList;
        }
        //OK
        public async Task<IActionResult> RemoveFromWatchlist(string imdbId, Guid userId)
        {
            try
            {
                _movieService.RemoveFromWatchListAsync(imdbId, userId);
                return Json(userId);
            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
        }
        public async Task<IActionResult> Watchlist(Guid userId)
        {
            var res = await _movieService.GetAllFromWatchList(userId);
            return View(res);
        }
        //OK
        public async Task<IActionResult> RateMovie(string imdbId, Guid userId, int rating, string comment)
        {
            try
            {
                _movieService.AddMovieRating(imdbId, userId, rating, comment);
                return Json(userId);
            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
            
        }
        //OK
        public async Task<IActionResult> RemoveMovieRating(string imdbId, Guid userId, int rating, string comment)
        {
            try
            {
                _movieService.RemoveMovieRating(imdbId, userId);
                return Json(userId);
            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
            
        }
        //NECESITAMOS EL INCLUDE
        public async Task<UserRating> GetUserRating(string imdbId, Guid userId)
        {

            UserRating rating = _movieService.GetMovieRating(imdbId, userId);
            return rating;
        }
    }
}
