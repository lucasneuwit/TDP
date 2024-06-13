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
            
            var movieDto = new MovieDTO();
            try 
            {
                var res = await _movieService.GetMovie(id);
                if (res == null)
                {
                    IRequest aRequest = new Request(null, id, type, releaseYear);
                    var movie = await _provider.FindAsync(aRequest);
                    if (movie is SeriesDTO)
                    {
                        _movieService.SaveSerie((SeriesDTO)movie);
                        var movieDb = await _movieService.GetMovie(id);
                        movieDto = _mapper.Map<SeriesDTO>(movieDb);
                        movieDto.IsAddedToWatchList = false;
                    }
                    else
                    {
                        _movieService.SaveMovie(movie);
                        var movieDb = await _movieService.GetMovie(id);
                        movieDto = _mapper.Map<MovieDTO>(movieDb);
                        movieDto.IsAddedToWatchList = false;
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
                return View("Error", new ErrorViewModel());
            }
            

        }
        [HttpGet]
        public async Task<IActionResult> Search(string title, string? type, string? releaseYear, int pageNumber)
        {
            IRequest aRequest = new Request(title, null, type, releaseYear);
            var res = await _provider.SearchAsync(aRequest, pageNumber);
            return View(res);
        }
        public async Task<IActionResult> GetMovie(string imdbId)
        {
            var movie = await _movieService.GetMovie(imdbId);
            var movieDto = _mapper.Map<MovieDTO>(movie);
            return Json(movieDto);
        }
        public async Task<IActionResult> GetAllMovies()
        {
            List<MovieDTO> movielist;
            var movies = await _movieService.GetAllMovies();
            movielist = _mapper.Map<List<MovieDTO>>(movies);
            return Json(movielist);
        }
        public async Task AddToWishList(Guid movieId, Guid userId, bool isInWatchList)
        {
            if (!isInWatchList)
            {
                await _movieService.AddToWatchListAsync(movieId, userId);
            }
            else NotFound();
        }
        public async Task<Boolean> AddedToWishList(Guid movieId, Guid userId)
        {
            bool isInWatchList = _movieService.AddedToWishList(movieId, userId);
            return isInWatchList;
        }
        public async Task RemoveFromWatchlist(Guid movieId, Guid userId)
        {
            await _movieService.RemoveFromWatchListAsync(movieId, userId);
        }
        public async Task<IActionResult> Watchlist(Guid userId)
        {
            var res = await _movieService.GetAllFromWatchList(userId);
            return View(res);
        }
        public async Task RateMovie(Guid movieId, Guid userId, int rating, string comment)
        {
            await _movieService.AddMovieRating(movieId, userId, rating, comment);
        }
        public async Task RemoveMovieRating(Guid movieId, Guid userId, int rating, string comment)
        {
            await _movieService.RemoveMovieRating(movieId, userId, rating, comment);
        }
        public async Task<UserRating> GetUserRating(Guid movieId, Guid userId)
        {
            UserRating rating = _movieService.GetMovieRating(movieId, userId);
            return rating;
        }
    }
}
