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
        public async Task<IActionResult> FindByTitle(String title, String? type, String? releaseYear)
        {
            IRequest aRequest = new Request(title,null,type,releaseYear);
            var res = await _provider.FindAsync(aRequest);
            return Json(res);
        }
        public async Task<IActionResult> FindById(String id, String? type, String? releaseYear)
        {
            IRequest aRequest = new Request(null, id, type, releaseYear);
            var res = await _provider.FindAsync(aRequest);
            return Json(res);
        }
        public async Task<IActionResult> Search(String title, String? type, String? releaseYear, int pageNumber)
        {
            IRequest aRequest = new Request(title, null, type, releaseYear);
            var res = await _provider.SearchAsync(aRequest,pageNumber);
            return Json(res);
        }
    }
}
