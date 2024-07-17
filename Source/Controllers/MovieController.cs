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
        public async Task<IActionResult> AddToWatchlist(string imdbId, bool isInWatchList)
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
        //OK
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
        //OK
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
        //OK
        public async Task<IActionResult> RemoveMovieRating(string imdbId, int rating, string comment)
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
