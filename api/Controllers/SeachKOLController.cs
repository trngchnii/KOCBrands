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
        public IActionResult Index(string name, string? gender, DateTime? dateOfBirth,  int? followersCount, decimal? bookingPrice)
        {
            IEnumerable<InfluencerDto> results = _searchKOLRepository.SearchKOL(name, gender, dateOfBirth, followersCount, bookingPrice);

            return Ok(results);
        }

		[HttpGet("getKOCs")]
		public IActionResult GetKOCs()
		{
			var kocs = _searchKOLRepository.GetAllKOCs();
			return Ok(kocs);
		}
	}
}
