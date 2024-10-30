using api.Data;
using api.DTOs;
using api.DTOs.CategoryDTO;
using api.DTOs.Pagination;
using api.DTOs.User;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebClient.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{action=Dashboard}")]

    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string CategoryAPIURL = "https://localhost:7290/odata/Category";
        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Dashboard()
        {
            return View();
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
    }


}
