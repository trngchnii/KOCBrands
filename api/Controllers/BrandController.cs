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
    [Route("api/[controller]")]
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

        // PUT: api/brand/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BrandDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brandEntity = new Brand
            {
                BrandName = updateDto.BrandName,
                ImageCover = updateDto.ImageCover,
                CategoryId = updateDto.CategoryId
            };

            var userEntity = new User
            {
                Email = updateDto.User.Email,
                Avatar = updateDto.User.Avatar,
                Bio = updateDto.User.Bio,
                Phonenumber = updateDto.User.Phonenumber,
                Address = updateDto.User.Address
            };

            var (updatedBrand, updatedUser) = await _brandRepo.UpdateAsync(id, brandEntity, userEntity);

            if (updatedBrand == null)
            {
                return NotFound(new { message = "Brand not found." });
            }

            return Ok(new { updatedBrand, updatedUser });
        }

        // GET: api/brand
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandRepo.GetAllAsync();
            if (brands == null )
            {
                return NotFound(new { message = "No brands found." });
            }

            return Ok(brands);  // Return the list of brands
        }

        // GET: api/brand/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound(new { message = "Brand not found." });
            }

            return Ok(brand);  // Return the brand details
        }
    }
}
