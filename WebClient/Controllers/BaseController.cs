using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebClient.Areas.Admin.Data;

namespace WebClient.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly string CategoryAPIURL;
        protected readonly string BrandAPIURL;
        protected readonly string InfluencerAPIURL;
        public BaseController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            var apiSettings = new ApiSettings();
            configuration.GetSection("ApiUrls").Bind(apiSettings);

            CategoryAPIURL = apiSettings.CategoryAPIURL;
            BrandAPIURL = apiSettings.BrandAPIURL;
            InfluencerAPIURL = apiSettings.InfluencerAPIURL;
        }

    }
}
