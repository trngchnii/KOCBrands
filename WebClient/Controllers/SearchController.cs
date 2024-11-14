using api.Data;
using api.DTOs;
using api.Models;
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
            string apiUrl = "https://localhost:7290/odata/SeachKOL/";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var kocs = JsonConvert.DeserializeObject<IEnumerable<InfluencerDto>>(jsonResponse);

				foreach (var influencer in kocs)
				{
					if (influencer.User != null && string.IsNullOrEmpty(influencer.User.Avatar))
					{
						influencer.User.Avatar = "default-avatar.jpg";
					}
                    if (influencer.SocialMedias != null)
                    {
                        foreach (var socialMedia in influencer.SocialMedias)
                        {
                            if (socialMedia.FollowersCount == null || socialMedia.FollowersCount == 0)
                            {
                                socialMedia.FollowersCount = 1000;
                            }
                        }
                    }
                }
                
                return View(kocs);
            }

            return View(new List<InfluencerDto>());
        }

        [HttpPost]
		public async Task<IActionResult> Search(string name, string? gender, DateTime? dateOfBirth, decimal? bookingPrice = null, int? personalIdentificationNumber = null, string? sorting = null)
        {
            string apiUrl = $"https://localhost:7290/odata/SeachKOL/search?name={name}&gender={gender}&dateOfBirth={dateOfBirth}&bookingPrice={bookingPrice}&personalIdentificationNumber={personalIdentificationNumber}&sorting={sorting}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            IEnumerable<InfluencerDto> influencers = Enumerable.Empty<InfluencerDto>();

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                influencers = JsonConvert.DeserializeObject<IEnumerable<InfluencerDto>>(jsonResponse);

				foreach(var influencer in influencers)
				{
					if (influencer.User != null && string.IsNullOrEmpty(influencer.User.Avatar))
					{
						influencer.User.Avatar = "default-avatar.jpg";
					}
                    if (influencer.SocialMedias != null)
                    {
                        foreach (var socialMedia in influencer.SocialMedias)
                        {
                            if (socialMedia.FollowersCount == null || socialMedia.FollowersCount == 0)
                            {
                                socialMedia.FollowersCount = 1000;
                            }
                        }
                    }
                }
			}

            return View("Index", influencers);
        }
    }
}
