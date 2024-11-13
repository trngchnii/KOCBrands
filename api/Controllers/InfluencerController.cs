using api.DTOs;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace api.Controllers
{
    [Route("odata/Influencers")]
    [ApiController]
    public class InfluencerController : ODataController
    {
        private readonly IInfluencerRepository _influencerRepo;
        public InfluencerController(IInfluencerRepository influencerRepo)
        {
            _influencerRepo = influencerRepo;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Influencer>>> Get()
        {
            var list = await _influencerRepo.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{key}")]
        public async Task<ActionResult> Get([FromODataUri] int key)
        {
            var influencer = await _influencerRepo.GetByIdAsync(key);

            if (influencer == null)
            {
                return NotFound();
            }

            return Ok(influencer);
        }


        [HttpPut("{influencerId}")]
        public async Task<IActionResult> UpdateInfluencer(int influencerId,[FromForm] UpdateInfluencerRequestDto requestDto)
        {
            try
            {
                await _influencerRepo.UpdateInfluencerAsync(influencerId,requestDto);

                return Ok(new { message = "Influencer updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Influencer influ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _influencerRepo.AddAsync(influ);

            // Trả về thông tin của influencer vừa được tạo
            return CreatedAtAction(nameof(Get),new { key = influ.InfluencerId },influ);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var existInflu = await _influencerRepo.GetByIdAsync(key);
            if (existInflu == null)
            {
                return NotFound();
            }

            await _influencerRepo.DeleteAsync(existInflu.InfluencerId);

            return NoContent();
        }
    }
}