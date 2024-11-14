using api.Models;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Text;
using WebClient.Models;
using api.DTOs.Campaign;

namespace WebClient.Controllers
{
    public class ProposalController : BaseController
    {
        private readonly PayOS _payOS;
        public ProposalController(IConfiguration configuration, PayOS payOS) : base(configuration)
        {
            _payOS = payOS;
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
                    return Json(new { success = true,message = "Proposal added successfully!" });
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


        public async Task<IActionResult> Accept(int cid,int id,string name,double price)
        {
            string apiUrl = $"https://localhost:7290/odata/Campaigns({id})";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var cate = JsonConvert.DeserializeObject<CampaignEdit>(jsonResponse);
            string Telephone = "0915197774";
            string Address = "Da Nang";
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            List<ItemData> items = new List<ItemData> { new ItemData(cate.Title,1,10000) };
            PaymentData paymentData = new PaymentData(orderCode,10000,"Thanh toan tien KOL",items,"https://" + Request.Host + "/Proposal/HandleCheckout","https://" + Request.Host + "/Proposal/HandleCheckout",null,name,null,Telephone,Address);
            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
            TempData["pid"] = id;
            TempData["cid"] = cid;
            return Redirect(createPayment.checkoutUrl);
            //return RedirectToAction("HandleCheckout");

        }
        public async Task<IActionResult> HandleCheckout(string code = null, string id = null, string cancel = null, string status = null, string orderCode = null)
        {
            int pid = TempData.ContainsKey("pid") ? (int)TempData["pid"] : 0;
            int cid = TempData.ContainsKey("cid") ? (int)TempData["cid"] : 0;

            if (status == "CANCELLED")
            {
                return RedirectToAction("ProposalList", new { id = cid });
            }
            else
            {
                string baseUrl = "https://localhost:7290/odata/Proposals";
                string requestUrl = $"{baseUrl}/{pid}";
                HttpResponseMessage res = await _httpClient.GetAsync(requestUrl);
                if (!res.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                Proposal proposal = JsonConvert.DeserializeObject<Proposal>(await res.Content.ReadAsStringAsync());
                proposal.Status = "Đã duyệt";
                var json = JsonConvert.SerializeObject(proposal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage res2 = await _httpClient.PutAsync(requestUrl, content);
                if (!res2.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
                return RedirectToAction("ProposalList", new { id = cid });

            }
        }

        public async Task<IActionResult> Reject(int cid, int id, string name, int price)
        {
            string baseUrl = "https://localhost:7290/odata/Proposals";
            string requestUrl = $"{baseUrl}/{id}";
            HttpResponseMessage res = await _httpClient.GetAsync(requestUrl);
            if (!res.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            Proposal proposal = JsonConvert.DeserializeObject<Proposal>(await res.Content.ReadAsStringAsync());
            proposal.Status = "Đã từ chối";
            var json = JsonConvert.SerializeObject(proposal);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage res2 = await _httpClient.PutAsync(requestUrl, content);
            if (!res2.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            return RedirectToAction("ProposalList", new { id = cid });
        }

        public class ProposalResponse
        {
            public List<Proposal> Value { get; set; }
            public int Count { get; set; }
        }
    }
}
