using System.Diagnostics;
using api.DTOs;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebClient.Models;
using api.Models;

namespace WebClient.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index(string? code = null, string? id = null, string? cancel = null, string? status = null, string? orderCode = null)
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
