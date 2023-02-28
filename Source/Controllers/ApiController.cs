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
            IRequest aRequest = new Request();
            aRequest.Title = title;

            var res = await _provider.FindAsync(aRequest);
            return Json(res);
        }
        public async Task<IActionResult> Search()
        {
            return View();
        }
    }
}
