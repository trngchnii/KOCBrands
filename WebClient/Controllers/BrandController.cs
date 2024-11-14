using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebClient.Models;


namespace WebClient.Controllers
{
    public class BrandController : BaseController
    {
        
        public BrandController(IConfiguration configuration) : base(configuration)
        {
        }
        public class BrandResponse
        {
            [JsonProperty("value")]
            public List<Brand> Brands { get; set; } = new();

            
        }

        public class CampaignResponse
        {
            [JsonProperty("value")]
            public List<Campaign> Campaigns { get; set; } = new();

        }

        public async Task<IActionResult> BrandProfile()
        {

            var brandId = HttpContext.Request.Cookies["BrandId"];


            if (string.IsNullOrEmpty(brandId))
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            
            string str = BrandAPIURL;
            HttpResponseMessage res = await _httpClient.GetAsync($"https://localhost:7290/odata/Brands?$filter=BrandId eq {brandId}&$expand=User,Campaigns");

            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<BrandResponse>(rData);
            var brand = response.Brands.FirstOrDefault();


            
            HttpResponseMessage resCam = await _httpClient.GetAsync($"https://localhost:7290/odata/Campaigns?$filter=BrandId eq {brandId}");

            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string cData = await resCam.Content.ReadAsStringAsync();
            var responseCam = JsonConvert.DeserializeObject<CampaignResponse>(cData);
            var campaign = responseCam.Campaigns;


            var profile = new BrandProfileVM()  
            {
                Brand = brand,
                Campaigns = campaign
            };
            return View(profile);
        }
        public IActionResult EditProfile()
        {
            return View();
        }
    }
}