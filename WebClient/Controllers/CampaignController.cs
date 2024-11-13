using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class CampaignController : BaseController
    {
        public CampaignController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpGet]
        public async Task<IActionResult> CreateCampaign()
        { 
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCampaign(Campaign campaign) 
        {

            return View();
        }



    }
}
