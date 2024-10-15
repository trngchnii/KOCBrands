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

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string name, string? gender, DateTime? dateOfBirth, int? followersCount = null, decimal? bookingPrice = null)
        {
            string apiUrl = $"https://localhost:7290/searchKOL/search?name={name}&gender={gender}&dateOfBirth={dateOfBirth}&followersCount={followersCount}&bookingPrice={bookingPrice}";
            
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var influencers = JsonConvert.DeserializeObject<IEnumerable<InfluencerDto>>(jsonResponse);

                return View("SearchResults", influencers);
            } 
            
            return View("SearchFail");
        }


    }
}
