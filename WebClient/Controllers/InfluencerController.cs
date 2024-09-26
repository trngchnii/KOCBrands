using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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