using api.Data;
using api.DTOs;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace api.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class SeachKOLController : ODataController
    {
        private readonly ISearchKOLRepository _searchKOLRepository;

        public SeachKOLController(ISearchKOLRepository searchKOLRepository)
        {
            _searchKOLRepository = searchKOLRepository;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Influencer>>> Get()
        {
            var influencers =  await _searchKOLRepository.GetAllKOCs();

            if(influencers == null)
            {
                return NotFound();
            }

            return Ok(influencers);
        }

        [EnableQuery]
        [HttpGet("{key:int}")]
        public async Task<ActionResult> Get([FromODataUri] int key)
        {
            var influencers = await _searchKOLRepository.GetByIdAsync(key);

            if (influencers == null)
            {
                return NotFound();
            }

            return Ok(influencers);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name, [FromQuery] string? gender, [FromQuery] DateTime? dateOfBirth, [FromQuery] decimal? bookingPrice, [FromQuery] int? personalIdentificationNumber)
        {
            var results = _searchKOLRepository.SearchKOL(name, gender, dateOfBirth, bookingPrice, personalIdentificationNumber);

            if( results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }
    }
}
