using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace api.Controllers
{
    [Route("odata/Proposals")]
    [ApiController]
    public class ProposalController : ODataController
    {
        private readonly IProposalRepository _proposalRepo;
        public ProposalController(IProposalRepository proposalRepo)
        {
            _proposalRepo = proposalRepo;
        }
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proposal>>> Get()
        {
            var list = await _proposalRepo.GetAllAsync();
            return Ok(list);
        }


        [HttpGet("{key}")]
        public async Task<ActionResult> Get([FromODataUri] int key)
        {
            var prop = await _proposalRepo.GetByIdAsync(key);

            if (prop == null)
            {
                return NotFound();
            }

            return Ok(prop);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Proposal prop)
        {
            if (!ModelState.IsValid || prop == null)
            {
                return BadRequest(ModelState);
            }

            await _proposalRepo.UpdateAsync(prop);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Proposal prop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _proposalRepo.AddAsync(prop);

            return Created(prop);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {

            var prop = await _proposalRepo.GetByIdAsync(key);
            if (prop == null)
            {
                return NotFound();
            }

            await _proposalRepo.DeleteAsync(prop.ProposalId);

            return NoContent();
        }
    }
}
