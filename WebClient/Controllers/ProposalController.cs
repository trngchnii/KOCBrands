using api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class ProposalController : BaseController
    {
        public ProposalController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpPost]
        public async Task<IActionResult> AddProposal(Proposal proposal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var json = JsonConvert.SerializeObject(proposal);
                var content = new StringContent(json,Encoding.UTF8,"application/json");
                string apiUrl = "https://localhost:7290/odata/Proposals";

                HttpResponseMessage res = await _httpClient.PostAsync(apiUrl,content);

                if (res.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Proposal added successfully!" });
                }
                else
                {
                    // If the request fails, return an error message in the response body
                    var errorMessage = await res.Content.ReadAsStringAsync();
                    return StatusCode((int)res.StatusCode,new { message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding proposal: " + ex.Message);
                return StatusCode(500,"An error occurred while processing your request.");
            }
        }
        public async Task<IActionResult> ProposalList(int id,int page = 1,int pageSize = 10)
        {
            string baseUrl = "https://localhost:7290/odata/Proposals";
            // Sử dụng $expand để lấy thông tin Influencer
            string requestUrl = $"{baseUrl}?$filter=CampaignId eq {id}&$top={pageSize}&$skip={(page - 1) * pageSize}&$expand=Influencer";

            HttpResponseMessage res = await _httpClient.GetAsync(requestUrl);
            if (!res.IsSuccessStatusCode)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string rData = await res.Content.ReadAsStringAsync();
            var responseWrapper = JsonConvert.DeserializeObject<ProposalResponse>(rData);

            // Lấy tổng số lượng để phân trang
            string countUrl = $"{baseUrl}?$filter=CampaignId eq {id}";
            HttpResponseMessage countRes = await _httpClient.GetAsync(countUrl);
            if (!countRes.IsSuccessStatusCode)
            {
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            string countData = await countRes.Content.ReadAsStringAsync();
            var countWrapper = JsonConvert.DeserializeObject<ProposalResponse>(countData);

            int totalCount = countWrapper?.Value?.Count ?? 0;

            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewData["CurrentPage"] = page;

            return View(responseWrapper?.Value); // Trả về view với danh sách đề xuất
        }




        public class ProposalResponse
        {
            public List<Proposal> Value { get; set; }
            public int Count { get; set; }
        }
    }
}
