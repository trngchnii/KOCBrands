using api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebClient.Controllers
{
    public class SearchController : Controller
    {
        private readonly HttpClient _httpClient;

        public SearchController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(string name, string? gender, int? followersCount = null, decimal? bookingPrice = null)
        {
            return View();

            //string apiUrl = $"https://localhost:7290/searchKOL/search";

            //HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonResponse = await response.Content.ReadAsStringAsync();
            //    var influencers = JsonConvert.DeserializeObject<IEnumerable<InfluencerDto>>(jsonResponse);
            //    return View(influencers);
            //}

            //return View("Error");
        }
    }
}
