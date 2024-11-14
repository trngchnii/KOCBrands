using api.DTOs.Campaign;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using WebClient.Models;

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
            string apiUrl = $"https://localhost:7290/odata/Category";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            List<Category> category = new List<Category>();

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<List<Category>>(jsonResponse);
            }
            var model = new CreateCampaignVM()
            {
                Categories = category,
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCampaign(CreateCampaignVM campaign)
        {
            var brandId = HttpContext.Request.Cookies["BrandId"];
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7290/odata/Category/detail?key={campaign.CategoryID}");

            Category category = new();

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<Category>(jsonResponse);
            }
            var categoryList = new List<Category>();
            categoryList.Add(category);
            var campagin = new CreateCampaingDTO()
            {
                BrandId = int.Parse(brandId!),
                Title = campaign.Title,
                Description = campaign.Description,
                Budget = campaign.Budget,
                StartDate = campaign.StartDate,
                Requirements = campaign.Requirements,
                EndDate = campaign.EndDate,
                Status = true,
                FaviconId = 0
            };

            var json = JsonConvert.SerializeObject(campagin);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await _httpClient.PostAsync($"https://localhost:7290/odata/Campaigns", content);
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public class CampaignResponse
        {
            [JsonProperty("value")]
            public List<Campaign> Value { get; set; } = new List<Campaign>();
        }
        public async Task<IActionResult> Details(int id)
        {
            /*string str = "";
            str = "https://localhost:7290/odata/Campaigns";
            HttpResponseMessage res = await _httpClient.GetAsync($"{str}({id})");
            if (!res.IsSuccessStatusCode)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Category>(rData);

            return View(response);*/
            return View();
        }

        public async Task<IActionResult> Index()
        {
            string str = "";
            str = "https://localhost:7290/odata/Campaigns";
            HttpResponseMessage res = await _httpClient.GetAsync(str);
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<CampaignResponse>(rData);

            return View(response.Value);
        }
    }
}
