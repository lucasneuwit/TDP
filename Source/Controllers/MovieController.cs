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
        
        public async Task<IActionResult> FindById(string id, string? type, string? releaseYear)
        {
            try 
            {
                MovieDTO movieDto;
                var resultFromLocalStorage = await _movieService.GetMovie(id);
                if (resultFromLocalStorage is not null)
                {
                    if (resultFromLocalStorage is Series)
                    {
                        movieDto = _mapper.Map<SeriesDTO>(resultFromLocalStorage);
                    }
                    else
                    {
                        movieDto = _mapper.Map<MovieDTO>(resultFromLocalStorage);
                    }
                }
                else
                {
                    IRequest aRequest = new Request(null, id, type, releaseYear);
                    movieDto = _provider.FindAsync(aRequest).Result;
                    if (movieDto is SeriesDTO)
                    {
                        await _movieService.SaveSeries((SeriesDTO)movieDto);
                        
                    }
                    else
                    {
                        await _movieService.SaveMovie(movieDto);
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
        
        public async Task<IActionResult> AddToWatchlist(string imdbId)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                await _movieService.AddToWatchListAsync(imdbId, currentUserId);
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
                bool isInWatchList = await _movieService.AddedToWatchList(imdbId, currentUserId);
                return isInWatchList;
            }
            catch(ArgumentNullException)
            {
                return false;
            }
        }

        
        public async Task<IActionResult> RemoveFromWatchlist(string imdbId)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                await _movieService.RemoveFromWatchListAsync(imdbId, currentUserId);
                return Json(currentUserId);
            }
            catch (MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
        }

        
        public async Task<IActionResult> Watchlist()
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

        
        public Task<IActionResult> RateMovie(string imdbId, int rating, string comment)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                _movieService.AddMovieRating(imdbId, currentUserId, rating, comment);
                return Task.FromResult<IActionResult>(Json(new { success = true, message = "Movie rated successfully." }));

            }
            catch (MovieNotFoundException ex)
            {
                return Task.FromResult<IActionResult>(Json(new { success = false, message = ex.Message }));
            }
        }
        
        
        public async Task<IActionResult> RemoveMovieRating(string imdbId)
        {
            try
            {
                Guid currentUserId = HttpContext.GetCurrentUserId();
                await _movieService.RemoveMovieRating(imdbId, currentUserId);
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
                var rating = await _movieService.GetMovieRating(imdbId, currentUserId);
                return Json(rating);
            }
            catch(MovieNotFoundException ex)
            {
                return View("MovieError", new MovieErrorViewModel { ErrorMessage = ex.Message });
            }
        }
    }
}
