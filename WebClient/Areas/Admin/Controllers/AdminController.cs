using Microsoft.AspNetCore.Mvc;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{action=Dashboard}")]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Analytics()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            return View();
        }

        public IActionResult ProductManagement()
        {
            return View();
        }
    }
}
