using api.DTOs;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace api.Controllers
{
    [Route("odata/Category")]
    [ApiController]
    public class CategoryController : ODataController
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var list = await _categoryRepo.GetAllAsync();
            return Ok(list);
        }


        [HttpGet("detail")]
        public async Task<ActionResult> Get([FromODataUri] int key)
        {
            var cate = await _categoryRepo.GetByIdAsync(key);

            if (cate == null)
            {
                return NotFound();
            }

            return Ok(cate);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Category category)
        {
            if (!ModelState.IsValid || category == null)
            {
                return BadRequest(ModelState);
            }

            await _categoryRepo.UpdateAsync(category);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Category cate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _categoryRepo.AddAsync(cate);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {

            var existCate = await _categoryRepo.GetByIdAsync(key);
            if (existCate == null)
            {
                return NotFound();
            }

            await _categoryRepo.DeleteAsync(existCate.CategoryId);

            return Ok();
        }

    }
}
