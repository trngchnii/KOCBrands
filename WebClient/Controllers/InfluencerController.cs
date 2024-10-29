using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class InfluencerController : Controller
    {
        private readonly ILogger<InfluencerController> _logger;

        public InfluencerController(ILogger<InfluencerController> logger)
        {
            _logger = logger;
        }


        public IActionResult EditProfile()
        {
            return View();
        }
    }
}