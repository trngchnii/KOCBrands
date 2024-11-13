using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebClient.Models;


namespace WebClient.Controllers
{
    public class BrandController : BaseController
    {
        public class InfluencerResponse
        {
            [JsonProperty("value")]
            public List<Influencer> Value { get; set; }
        }
        public BrandController(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IActionResult> BrandProfile()
        {
            
            var brandId = HttpContext.Request.Cookies["BrandId"];

            
            if (string.IsNullOrEmpty(brandId))
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            // G?i API ð? l?y thông tin c?a Influencer d?a trên InfluencerId t? cookie
            string str = BrandAPIURL;
            HttpResponseMessage res = await _httpClient.GetAsync($"{str}{brandId}");

            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Brand>(rData);

            return View(response);
        }
        public IActionResult EditProfile()
        {
            return View();
        }
    }
}