using api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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

    }
}
