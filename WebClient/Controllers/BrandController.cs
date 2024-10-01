using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace WebClient.Controllers
{
    public class BrandController : Controller
    {
        private readonly ILogger<BrandController> _logger;

        public BrandController(ILogger<BrandController> logger)
        {
            _logger = logger;
        }


        public IActionResult EditProfile()
        {
            return View();
        }

        public IActionResult BandProfile()
        {
            return View();
        }
    }
}