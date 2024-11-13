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
            string baseUrl = "https://localhost:7290/odata/Campaigns";
            string requestUrl = $"{baseUrl}?$filter=CampaignId eq {id}&$expand=Brand($expand=User),Categories";

            HttpResponseMessage res = await _httpClient.GetAsync(requestUrl);

            if (!res.IsSuccessStatusCode)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();

            // Since OData responses are usually wrapped in a "value" array, we should handle it accordingly
            var responseWrapper = JsonConvert.DeserializeObject<CampaignResponse>(rData);
            var campaign = responseWrapper?.Value?.FirstOrDefault(); // Get the first campaign if it exists

            if (campaign == null)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return View(campaign);
        }

        public async Task<IActionResult> Index()
        {
            string str = "";
            str = "https://localhost:7290/odata/Campaigns";
            HttpResponseMessage res = await _httpClient.GetAsync($"{str}?$expand=Brand($expand=User),Categories");
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
