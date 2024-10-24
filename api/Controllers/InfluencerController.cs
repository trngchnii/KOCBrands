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

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key,[FromBody] Influencer influ)
        {
            if (!ModelState.IsValid || influ == null)
            {
                return BadRequest(ModelState);
            }

            // Map User information
            var userModel = new User
            {
                Email = influ.User.Email,
                Avatar = influ.User.Avatar,
                Bio = influ.User.Bio,
                Phonenumber = influ.User.Phonenumber,
                Address = influ.User.Address
            };

            // Call repository to update the brand and user
            var result = await _influencerRepo.UpdateAsync(key,influ,userModel);

            if (result.Item1 == null)
            {
                return NotFound("Brand not found.");
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Influencer influ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _influencerRepo.AddAsync(influ);
            return Created(influ);
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


        /*[HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] InfluencerDto updateDto)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Prepare the Brand and User entities for the update
            var influencerEntity = new Influencer
            {
                Name = updateDto.Name,
                Gender = updateDto.Gender,
                DateOfBirth = updateDto.DateOfBirth,
                SocialMedias = updateDto.SocialMedias,
                BookingPrice = updateDto.BookingPrice,
                PersonalIdentificationNumber = updateDto.PersonalIdentificationNumber,
            };

            var userEntity = new User
            {
                Email = updateDto.User.Email,
                Avatar = updateDto.User.Avatar,
                Bio = updateDto.User.Bio,
                Phonenumber = updateDto.User.Phonenumber,
                Address = updateDto.User.Address
            };

            // Call the repository to update
            var (updatedInfluencer, updatedUser) = await _influencerRepo.UpdateAsync(id, influencerEntity, userEntity);

            // Handle the case where the brand or user was not found
            if (updatedInfluencer == null)
            {
                return NotFound(new { message = "Brand not found." });
            }

            // Return the updated brand and user
            return Ok(new { updatedInfluencer, updatedUser });
        }*/
    }
}