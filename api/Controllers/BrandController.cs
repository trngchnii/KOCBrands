using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBrandRepository _brandRepo;
        public BrandController(ApplicationDbContext context, IBrandRepository brandRepo)
        {
            _context = context;
            _brandRepo = brandRepo;
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BrandDto updateDto)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Prepare the Brand and User entities for the update
            var brandEntity = new Brand
            {
                BrandName = updateDto.BrandName,
                ImageCover = updateDto.ImageCover,
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
            var (updatedBrand, updatedUser) = await _brandRepo.UpdateAsync(id, brandEntity, userEntity);

            // Handle the case where the brand or user was not found
            if (updatedBrand == null)
            {
                return NotFound(new { message = "Brand not found." });
            }

            // Return the updated brand and user
            return Ok(new { updatedBrand, updatedUser });
        }
    }
}