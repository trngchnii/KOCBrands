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
        public class InfluencerResponseEdit
        {
            [JsonProperty("value")]
            public List<Influencer> Influencers { get; set; } = new List<Influencer>();
        }
        public class InfluencerEditViewModel
        {
            public UpdateInfluencerRequestDto InfluencerEditDto { get; set; } // Dùng cho POST
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
                HttpResponseMessage res = await _httpClient.GetAsync($"https://localhost:7290/odata/Influencers?$filter=InfluencerId eq {influencerId}&$expand=User");

                if (!res.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                string rData = await res.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<InfluencerResponseEdit>(rData);

                var influencer = response.Influencers.FirstOrDefault();
                return View(influencer);
            }

        }
        

        /*public class InfluencerResponse
        {
            [JsonProperty("Id")]
            public int InfluencerId { get; set; }

            [JsonProperty("User")]
            public User User { get; set; }
        }*/
        private UpdateInfluencerRequestDto MapInfluencerToDto(Influencer influencer)
        {
            //ham gìiformFile TỪ ANH
            var file = "https://localhost:7290/" + influencer?.User?.Avatar;

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
                AvatarFile = null,
                RealAvatar = file
            };
        }

        public IFormFile GetIFormFileFromPath(string filePath)
        {
            // Mở file từ đường dẫn
            var fileStream = new FileStream(filePath,FileMode.Open,FileAccess.Read);

            // Tạo một IFormFile từ FileStream
            IFormFile formFile = new FormFile(fileStream,0,fileStream.Length,"file",Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream" // Hoặc bạn có thể chỉ định loại MIME tương ứng
            };

            return formFile;
        }

        // GET EditProfile
        public async Task<IActionResult> EditProfile()
        {
            var influencerId = HttpContext.Session.GetInt32("InfluencerId");
            if (influencerId == null)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

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
                InfluencerEditDto = influencerEditDto
            };

            return View(viewModel);
        }

        // POST EditProfile
        [HttpPost]
        public async Task<IActionResult> EditProfile(InfluencerEditViewModel influencer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var influencerId = HttpContext.Session.GetInt32("InfluencerId");
                    if (influencerId == null)
                    {
                        return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                    }

                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(influencer.InfluencerEditDto.Name ?? ""),"Name");
                    formData.Add(new StringContent(influencer.InfluencerEditDto.Gender ?? ""),"Gender");
                    formData.Add(new StringContent(influencer.InfluencerEditDto.DateOfBirth.ToString("yyyy-MM-dd") ?? ""),"DateOfBirth");
                    formData.Add(new StringContent(influencer.InfluencerEditDto.BookingPrice.ToString() ?? ""),"BookingPrice");
                    formData.Add(new StringContent(influencer.InfluencerEditDto.PersonalIdentificationNumber.ToString() ?? ""),"PersonalIdentificationNumber");
                    formData.Add(new StringContent(influencer.InfluencerEditDto.Email ?? ""),"Email");
                    formData.Add(new StringContent(influencer.InfluencerEditDto.Bio ?? ""),"Bio");
                    formData.Add(new StringContent(influencer.InfluencerEditDto.PhoneNumber ?? ""),"PhoneNumber");
                    formData.Add(new StringContent(influencer.InfluencerEditDto.Address ?? ""),"Address");

                    if (influencer.InfluencerEditDto.AvatarFile != null)
                    {
                        var avatarContent = new StreamContent(influencer.InfluencerEditDto.AvatarFile.OpenReadStream());
                        avatarContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(influencer.InfluencerEditDto.AvatarFile.ContentType);
                        formData.Add(avatarContent,"AvatarFile",influencer.InfluencerEditDto.AvatarFile.FileName);
                    }

                    HttpResponseMessage res = await _httpClient.PutAsync($"https://localhost:7290/odata/Influencers/{influencerId}",formData);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Đã cập nhật thông tin thành công";
                        return RedirectToAction("EditProfile"); // Chuyển hướng về trang EditProfile để hiển thị thông báo
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

                var viewModel = new InfluencerEditViewModel
                {
                    InfluencerEditDto = influencer.InfluencerEditDto
                };
                return View(viewModel);
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}