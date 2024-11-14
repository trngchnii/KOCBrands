using api.Data;
using api.DTOs;
using api.DTOs.Campaign;
using api.DTOs.CategoryDTO;
using api.DTOs.Pagination;
using api.DTOs.User;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Net.Http;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{action=Dashboard}")]


    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
    
        private readonly string CategoryAPIURL = "https://localhost:7290/odata/Category";
        private readonly string CampaignAPIURL = "https://localhost:7290/odata/Campaigns";
        private readonly PayOS _payOS;
        public AdminController(HttpClient httpClient, PayOS payOS)
        {
            _httpClient = httpClient;
            _payOS = payOS;
        }

        public async Task<IActionResult> Dashboard()
        {
            string apiUrl = CampaignAPIURL;
            int totalCampaign = 0;
            int totalBrand= 0;
            int totalInfluencer = 0;
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var campaigns = JsonConvert.DeserializeObject<CampaignResponse>(jsonResponse);
                totalCampaign = campaigns.Value.Count;
            }
            apiUrl = $"https://localhost:7290/api/admin/getalluser";

            response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var pagedResult = JsonConvert.DeserializeObject<List<User>>(jsonResponse);
                pagedResult.ForEach(user =>
                {
                    if (user.Role == "Brand")
                    {
                        totalBrand++;
                    }
                    else if (user.Role == "Influencer")
                    {
                        totalInfluencer++;
                    }
                }); 
            }
            var dashboard = new
            {
                TotalCampaign = totalCampaign,
                TotalBrand = totalBrand,
                TotalInfluencer = totalInfluencer
            };
            return View(dashboard);
        }

        public IActionResult Analytics()
        {
            return View();
        }

        public async Task<IActionResult> UserManagement(string? searchString, int pageNumber = 1, int pageSize = 5)
        {
            string apiUrl = $"https://localhost:7290/api/admin/getuser?searchString={searchString}&pageNumber={pageNumber}&pageSize={pageSize}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var pagedResult = JsonConvert.DeserializeObject<PagedResult<User>>(jsonResponse);
                ViewBag.SearchString = searchString;
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                return View(pagedResult);
            }
            return View();
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            string apiUrl = $"https://localhost:7290/api/admin/deleteuser?id={id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("UserManagement");
            }
            else
            {
                return View("Error");
            }

        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return PartialView("_CreateUserModal", new UserAdd());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserAdd user)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = "https://localhost:7290/api/admin/createuser";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserManagement");
                }
                else
                {
                    return View("Error");
                }
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            string apiUrl = $"https://localhost:7290/api/admin/getuserbyid?id={id}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserEdit>(jsonResponse);
                return PartialView("_EditUserModal", user);
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserEdit userEdit)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = "https://localhost:7290/api/admin/edituser";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(userEdit), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserManagement");
                }
                else
                {
                    return View("Error");
                }
            }

            return RedirectToAction("UserManagement");
        }

        public IActionResult ProductManagement()
        {
            return View();
        }
        public async Task<IActionResult> CategoryManagement()
        {
            string apiUrl = CategoryAPIURL;

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<Category>>(jsonResponse);
                return View(categories);
            }
            else
            {
                return View(new List<Category>());
            }
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            string apiUrl = $"{CategoryAPIURL}?key={id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryManagement");
            }
            return StatusCode((int)response.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryAdd cate)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = CategoryAPIURL;
                var jsonContent = new StringContent(JsonConvert.SerializeObject(cate), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryManagement");
                }
                else
                {
                    return View("Error");
                }
            }

            return RedirectToAction("CategoryManagement");
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            string apiUrl = $"{CategoryAPIURL}/detail?key={id}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var cate = JsonConvert.DeserializeObject<CategoryEdit>(jsonResponse);
                return PartialView("_EditCategoryModal", cate);
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryEdit cateEdit)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = CategoryAPIURL;
                Category category = new Category
                {
                    CategoryId = cateEdit.CategoryId,
                    CategoryName = cateEdit.CategoryName,
                    Description = cateEdit.Description
                };
                var jsonContent = new StringContent(JsonConvert.SerializeObject(category), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryManagement");
                }
                else
                {
                    return View("Error");
                }
            }

            return RedirectToAction("CategoryManagement");
        }
        public class CampaignResponse
        {
            [JsonProperty("value")]
            public List<Campaign> Value { get; set; } = new List<Campaign>();
        }
        public async Task<IActionResult> CampaignManagement()
        {
            string apiUrl = CampaignAPIURL;

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var campaigns = JsonConvert.DeserializeObject<CampaignResponse>(jsonResponse);
                return View(campaigns.Value);
            }
            else
            {
                return View(new List<Campaign>());
            }
        }

        public async Task<IActionResult> DeleteCampaign(int id)
        {
            string apiUrl = $"{CampaignAPIURL}({id})";
            HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CampaignManagement");
            }
            return StatusCode((int)response.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign(CampaignAdd camp)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = CampaignAPIURL + "/create-campaign";
                var jsonString = JsonConvert.SerializeObject(new
                {
                    BrandId = camp.BrandId,
                    Title = camp.Title,
                    Description = camp.Description,
                    Requirements = camp.Requirements,
                    Budget = camp.Budget,
                    StartDate = camp.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    EndDate = camp.EndDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    Status = camp.Status,
                    CreatedDate = camp.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ssZ")
                });
                var jsonContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("CampaignManagement");
                    }
                    else
                    {
                        // Lấy nội dung chi tiết của lỗi từ phản hồi API để kiểm tra
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    return View("Error");
                }


            }

            return RedirectToAction("CampaignManagement");
        }

        [HttpGet]
        public async Task<IActionResult> EditCampaign(int id)
        {
            string apiUrl = $"{CampaignAPIURL}({id})";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var cate = JsonConvert.DeserializeObject<CampaignEdit>(jsonResponse);
                return PartialView("_EditCampaignModal", cate);
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditCampaign(CampaignEdit camp)
        {
            if (ModelState.IsValid)
            {
                string apiUrl = CampaignAPIURL;
                var jsonString = JsonConvert.SerializeObject(new
                {
                    CampaignId = camp.CampaignId,
                    BrandId = camp.BrandId,
                    Title = camp.Title,
                    Description = camp.Description,
                    Requirements = camp.Requirements,
                    Budget = camp.Budget,
                    StartDate = camp.StartDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    EndDate = camp.EndDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    Status = camp.Status,
                    CreatedDate = camp.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ssZ")
                });
                var jsonContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("CampaignManagement");
                    }
                    else
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    return View("Error");
                }
            }

            return RedirectToAction("CampaignManagement");
        }
        //-----------------------------------------PAYMENT-----------------------------------------
        public async Task<IActionResult> TestPayment()
        {
            string Name = "tran van dat";
            string Telephone = "0915197774";
            string Address = "Da Nang";

            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            List<ItemData> items = new List<ItemData>
        {
            new ItemData("Apple",10,10000),
            new ItemData("Mango",5,5000),
            new ItemData("Grapes",8,5000)
            };
            PaymentData paymentData = new PaymentData(orderCode, 20000, "Thanh toan don hang", items, "http://" + Request.Host + "/cancel", "http://" + Request.Host + "/Home/Index", null, Name, null, Telephone, Address);
            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
            string baseUrl = new Uri(createPayment.checkoutUrl).GetLeftPart(UriPartial.Path);
            return Redirect(baseUrl);
        }

    }


}
