using api.Data;
using api.DTOs;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("searchKOL")]
    [ApiController]
    public class SeachKOLController : Controller
    {
        private readonly ISearchKOLRepository _searchKOLRepository;

        public SeachKOLController(ISearchKOLRepository searchKOLRepository)
        {
            _searchKOLRepository = searchKOLRepository;
        }

        [HttpGet("search")]
        public IActionResult Index(string name, string gender, int? followersCount, decimal? bookingPrice)
        {
            IEnumerable<InfluencerDto> results = _searchKOLRepository.SearchKOL(name, gender, followersCount, bookingPrice);

            return Ok(results);
        }
    }
}
