using Microsoft.AspNetCore.Mvc;
using TDP.Models.Application;

namespace TDP.Controllers
{
    public class ApiController : Controller
    {
        private readonly IApiProvider _provider;
        public ApiController(IApiProvider provider) 
        {
            _provider = provider;
        }
        public async Task<IActionResult> Find()
        {
            IRequest aRequest = new Request("Harry Potter", "movie", null);
            var res = await _provider.FindAsync(aRequest);
            return Json(res);
        }
        public async Task<IActionResult> FindByTitle(String title, String? type, String? releaseYear)
        {
            IRequest aRequest = new Request(title,type,releaseYear);
            var res = await _provider.FindAsync(aRequest);
            return Json(res);
        }
        public async Task<IActionResult> Search()
        {
            return View();
        }
    }
}
