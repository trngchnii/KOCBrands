using api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class CampaignController : BaseController
    {
        public CampaignController(IConfiguration configuration) : base(configuration)
        {
        }

        public class CampaignResponse
        {
            [JsonProperty("value")]
            public List<Campaign> Value { get; set; } = new List<Campaign>();
        }
        public async Task<IActionResult> Details(int id)
        {
            string str = "";
            str = "https://localhost:7290/odata/Campaigns";
            HttpResponseMessage res = await _httpClient.GetAsync($"{str}({id})");
            if (!res.IsSuccessStatusCode)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Category>(rData);

            return View(response);
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
