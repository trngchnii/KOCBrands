using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
