using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfluencerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IInfluencerRepository _influencerRepo;
        public InfluencerController(ApplicationDbContext context, IInfluencerRepository influencerRepo)
        {
            _context = context;
            _influencerRepo = influencerRepo;
        }
        [HttpPut]
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
                //SocialMediaLinks = updateDto.SocialMediaLinks,
                BookingPrice = updateDto.BookingPrice,
                PersonalIdentificationNumber = updateDto.PersonalIdentificationNumber,
                //CategoryId = updateDto.CategoryId
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

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var influencers = await _influencerRepo.GetAllAsync();
            if (influencers == null || !influencers.Any())
            {
                return NotFound(new { message = "No influencers found." });
            }

            return Ok(influencers);  // Return the list of influencers
        }

        // GET: api/influencer/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var influencer = await _influencerRepo.GetByIdAsync(id);
            if (influencer == null)
            {
                return NotFound(new { message = "Influencer not found." });
            }

            return Ok(influencer);  // Return the influencer details
        }
    }
}