using api.DTOs;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class InfluencerController : BaseController
    {
        public class InfluencerResponse
        {
            [JsonProperty("value")]
            public List<Influencer> Value { get; set; }
        }
        public InfluencerController(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IActionResult> ProfileDetails()
        {
            {
               
                var influencerId = HttpContext.Request.Cookies["InfluencerId"];

             
                if (string.IsNullOrEmpty(influencerId))
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                
                string str = InfluencerAPIURL;
                HttpResponseMessage res = await _httpClient.GetAsync($"{str}{influencerId}");

                if (!res.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                string rData = await res.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<UpdateInfluencerRequestDto>(rData);

                return View(response);
            }

        }

            public async Task<IActionResult> EditProfile()
        {
            // Lấy InfluencerId từ cookie
            var influencerId = HttpContext.Request.Cookies["InfluencerId"];

            // Kiểm tra nếu InfluencerId tồn tại trong cookie
            if (string.IsNullOrEmpty(influencerId))
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            // Gọi API để lấy thông tin của Influencer dựa trên InfluencerId từ cookie
            string str = InfluencerAPIURL;
            HttpResponseMessage res = await _httpClient.GetAsync($"{str}{influencerId}");

            if (!res.IsSuccessStatusCode)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<UpdateInfluencerRequestDto>(rData);

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UpdateInfluencerRequestDto influencer)
        {
            if (ModelState.IsValid)
            {
                // Lấy InfluencerId từ cookie
                var influencerId = HttpContext.Request.Cookies["InfluencerId"];

                if (string.IsNullOrEmpty(influencerId))
                {
                    return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                var json = JsonConvert.SerializeObject(influencer);
                var content = new StringContent(json,Encoding.UTF8,"application/json");

                string str = InfluencerAPIURL;
                HttpResponseMessage res = await _httpClient.PutAsync($"{str}{influencerId}",content);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); // Điều hướng đến một action phù hợp sau khi chỉnh sửa thành công
                }

                ModelState.AddModelError(string.Empty,"Server Error");
            }

            return View(influencer);
        }

    }
}