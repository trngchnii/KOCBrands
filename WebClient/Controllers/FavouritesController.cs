using api.DTOs;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebClient.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly HttpClient _httpClient;

        public FavouritesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {
            //var token = HttpContext.Session.GetString("Token"); // Lấy token từ session hoặc từ nguồn khác
            //if (string.IsNullOrEmpty(token))
            //{
            //    return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
            //}

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("https://localhost:7290/api/Favourites/list");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var favorites = JsonConvert.DeserializeObject<List<InfluencerDto>>(jsonResponse);

				foreach (var influencer in favorites)
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

				return View(favorites);
            }
            else
            {
                TempData["Error"] = "Không thể tải danh sách yêu thích.";
                return View(new List<InfluencerDto>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int influencerId)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"https://localhost:7290/api/Favourites/remove/{influencerId}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Đã xóa khỏi danh sách yêu thích.";
            }
            else
            {
                TempData["Error"] = "Không thể xóa khỏi danh sách yêu thích.";
            }

            return RedirectToAction("Index");
        }
    }
}
