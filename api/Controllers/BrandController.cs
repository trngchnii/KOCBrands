using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace api.Controllers
{
    [Route("odata/Brands")]
    [ApiController]
    public class BrandController : ODataController
    {
        private readonly IBrandRepository _brandRepo;
        public BrandController( IBrandRepository brandRepo)
        {
            _brandRepo = brandRepo;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> Get()
        {
            var list = await _brandRepo.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{key}")]
        public async Task<ActionResult> Get([FromODataUri] int key)
        {
            var brand = await _brandRepo.GetByIdAsync(key);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key,[FromBody] Brand brand)
        {
            if (!ModelState.IsValid || brand == null)
            {
                return BadRequest(ModelState);
            }

            // Map User information
            var userModel = new User
            {
                Email = brand.User.Email,
                Avatar = brand.User.Avatar,
                Bio = brand.User.Bio,
                Phonenumber = brand.User.Phonenumber,
                Address = brand.User.Address
            };

            // Call repository to update the brand and user
            var result = await _brandRepo.UpdateAsync(key,brand,userModel);

            if (result.Item1 == null)
            {
                return NotFound("Brand not found.");
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _brandRepo.AddAsync(brand);

            return Created(brand);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {

            var existBrand = await _brandRepo.GetByIdAsync(key);
            if (existBrand == null)
            {
                return NotFound();
            }

            await _brandRepo.DeleteAsync(existBrand.BrandId);

            return NoContent();
        }

/*
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
                ImageCover = updateDto.ImageCover
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

        // GET: api/brand
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandRepo.GetAllAsync();
            if (brands == null)
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
        }*/
    }
}
