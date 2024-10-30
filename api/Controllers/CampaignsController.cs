using api.DTOs;
using api.DTOs.Campaign;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace api.Controllers
{
    [Route("odata/Campaigns")]
    [ApiController]
    public class CampaignsController : ODataController
    {
        private readonly ICampainRepository _campainRepository;
        public CampaignsController(ICampainRepository campainRepository)
        {
            _campainRepository = campainRepository;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> Get()
        {
            var list = await _campainRepository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{key}")]
        public async Task<ActionResult> Get([FromODataUri] int key)
        {
            var campaign = await _campainRepository.GetByIdAsync(key);

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UpdateCampaignDto campaign)
        {
            if (!ModelState.IsValid || campaign == null)
            {
                return BadRequest(ModelState);
            }
            var result = await _campainRepository.UpdateAsync(key, campaign);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _campainRepository.AddAsync(campaign);
            return Created(campaign);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {

            var existsCampaign = await _campainRepository.GetByIdAsync(key);
            if (existsCampaign == null)
            {
                return NotFound();
            }

            await _campainRepository.DeleteAsync(existsCampaign.CampaignId);

            return NoContent();
        }
    }
}
