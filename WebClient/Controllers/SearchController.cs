using api.Data;
using api.DTOs;
using api.Repository;
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

        public async Task<ActionResult> Index()
        {
			string apiUrl = "https://localhost:7290/searchKOL/getAllKOCs";

			HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var kocs = JsonConvert.DeserializeObject<IEnumerable<InfluencerDto>>(jsonResponse);

				return View("Index", kocs);
			}

            return View("Index");
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
