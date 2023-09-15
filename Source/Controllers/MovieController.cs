using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TDP.Models.Application;
using TDP.Models.Application.Services;

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
                return View(movie);
            }
            else
            {
                var movieDto = _mapper.Map<MovieDTO>(res);
                return View(movieDto);
            }
        }
        public async Task<IActionResult> FindById(string id, string? type, string? releaseYear)
        {
            IRequest aRequest = new Request(null, id, type, releaseYear);
            var res = await _provider.FindAsync(aRequest);
            _movieService.SaveMovie(res);
            return View(res);
        }
        [HttpGet]
            
        public async Task<IActionResult> Search(string title, string? type, string? releaseYear, int pageNumber)
        {
            IRequest aRequest = new Request(title, null, type, releaseYear);
            var res = await _provider.SearchAsync(aRequest,pageNumber);
            return View(res);
        }

        public async Task<IActionResult> GetMovie(string title)
        {
            var movie = await _movieService.GetMovie(title);
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
    }
}
