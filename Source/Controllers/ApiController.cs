using Microsoft.AspNetCore.Mvc;
using TDP.Models.Application;
using TDP.Models.Application.DataTransfer;

namespace TDP.Controllers
{
    public class ApiController : Controller
    {
        private readonly IApiProvider _provider;
        public ApiController(IApiProvider provider) 
        {
            _provider = provider;
        }

        public async Task<IActionResult> FindByTitle(string title, string? type, string? releaseYear)
        {
            IRequest aRequest = new Request(title,null,type,releaseYear);
            var res = await _provider.FindAsync(aRequest);
            return View(res);
        }
        public async Task<IActionResult> FindById(string id, string? type, string? releaseYear)
        {
            IRequest aRequest = new Request(null, id, type, releaseYear);
            var res = await _provider.FindAsync(aRequest);
            return Json(res);
        }
        public async Task<IActionResult> Search(string title, string? type, string? releaseYear, int pageNumber)
        {
            IRequest aRequest = new Request(title, null, type, releaseYear);
            var res = await _provider.SearchAsync(aRequest,pageNumber);
            return View(res);
        }
    }
}
