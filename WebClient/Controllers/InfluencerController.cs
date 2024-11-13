using api.DTOs;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class InfluencerController : BaseController
    {

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
        public class InfluencerResponseEdit
        {
            [JsonProperty("value")]
            public List<Influencer> Influencers { get; set; } = new List<Influencer>();
        }
        public class InfluencerEditViewModel
        {
            public Influencer Influencer { get; set; } // Dùng cho GET
            public UpdateInfluencerRequestDto InfluencerEditDto { get; set; } // Dùng cho POST
        }

        public InfluencerController(IConfiguration configuration) : base(configuration)
        {
        }

        public class InfluencerResponse
        {
            [JsonProperty("Id")]
            public int InfluencerId { get; set; }

            [JsonProperty("User")]
            public User User { get; set; }
        }
        private UpdateInfluencerRequestDto MapInfluencerToDto(Influencer influencer)
        {
            return new UpdateInfluencerRequestDto
            {
                Name = influencer.Name,
                Gender = influencer.Gender,
                DateOfBirth = influencer.DateOfBirth,
                BookingPrice = influencer.BookingPrice,
                PersonalIdentificationNumber = influencer.PersonalIdentificationNumber,
                Email = influencer.User?.Email ?? string.Empty, // Đảm bảo xử lý nếu User là null
                Bio = influencer.User?.Bio ?? string.Empty,
                PhoneNumber = influencer.User?.Phonenumber ?? string.Empty,
                Address = influencer.User?.Address ?? string.Empty,
                AvatarFile = null
            };
        }


        // GET EditProfile
        public async Task<IActionResult> EditProfile()


        {
            var influencerId = HttpContext.Request.Cookies["InfluencerId"];

            if (string.IsNullOrEmpty(influencerId))
            {
                // Gửi thông báo lỗi nếu không tìm thấy InfluencerId trong cookie
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            /*
                        string apiUrl = $"https://localhost:7290/odata/Influencers/{influencerId}?$expand=User";*/
            string apiUrl = $"https://localhost:7290/odata/Influencers?$filter=InfluencerId eq {influencerId}&$expand=User";
            HttpResponseMessage res = await _httpClient.GetAsync(apiUrl);

            if (!res.IsSuccessStatusCode)
            {
                // Gửi thông báo lỗi nếu API không trả về thành công
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<InfluencerResponseEdit>(rData);
            var influencer = response?.Influencers[0];
            if (influencer == null)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            // Map dữ liệu của Influencer thành DTO
            var influencerEditDto = MapInfluencerToDto(influencer);

            // Tạo ViewModel để truyền cả Influencer và DTO cho View
            var viewModel = new InfluencerEditViewModel
            {
                Influencer = influencer,
                InfluencerEditDto = influencerEditDto
            };

            return View(viewModel);
        }

        // POST EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([FromForm] UpdateInfluencerRequestDto influencer)
        {
            if (ModelState.IsValid)
            {
                var influencerId = HttpContext.Request.Cookies["InfluencerId"];
                if (string.IsNullOrEmpty(influencerId))
                {
                    return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                var json = JsonConvert.SerializeObject(influencer);
                var content = new StringContent(json,Encoding.UTF8,"application/json");

                HttpResponseMessage res = await _httpClient.PutAsync($"{InfluencerAPIURL}/{influencerId}",content);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var errorContent = await res.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty,$"Có lỗi xảy ra khi cập nhật: {errorContent}");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Dữ liệu nhập vào không hợp lệ. Vui lòng kiểm tra lại.");
            }

            // Initialize Influencer to avoid null reference issues in the view
            var viewModel = new InfluencerEditViewModel
            {
                InfluencerEditDto = influencer,
                Influencer = new Influencer
                {
                    User = new User() // Initialize User to prevent null reference issues
                }
            };

            return View(viewModel);
        }
    }
}