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
    public class SeachKOLController : ODataController
    {
        private readonly ISearchKOLRepository _searchKOLRepository;

        public SeachKOLController(ISearchKOLRepository searchKOLRepository)
        {
            _searchKOLRepository = searchKOLRepository;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IEnumerable<InfluencerDto>> GetAll()
        {
            var influencers = _searchKOLRepository.GetAllKOCs();

            if(influencers == null)
            {
                return NotFound();
            }

            return Ok(influencers);
        }

        [EnableQuery]
        [HttpGet("{key:int}")]
        public async Task<ActionResult> GetById([FromODataUri] int key)
        {
            var influencers = await _searchKOLRepository.GetByIdAsync(key);

            if (influencers == null)
            {
                return NotFound();
            }

            return Ok(influencers);
        }

        [EnableQuery]
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name, [FromQuery] string? gender, [FromQuery] DateTime? dateOfBirth, [FromQuery] decimal? bookingPrice, [FromQuery] int? personalIdentificationNumber, [FromQuery] string? sorting)
        {
            var results = _searchKOLRepository.SearchKOL(name, gender, dateOfBirth, bookingPrice, personalIdentificationNumber, sorting);

            if( results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }
    }
}
