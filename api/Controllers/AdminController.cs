using api.Data;
using api.DTOs.Pagination;
using api.DTOs.User;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _db;


        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("getalluser")]   
        public IActionResult Get()
        {
            IEnumerable<User> list = _db.Users.ToList();
            return Ok(list);
        }
        [HttpGet]
        [Route("getuser")]
        public IActionResult Get(string? searchString,int pageNumber = 1,int pageSize = 5)
        {
            IEnumerable<User> list = _db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.UserName.Contains(searchString));
            }
            int totalCount = list.Count();
            var pagedUsers = list.Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();
            var result = new PagedResult<User>
            {
                Items = pagedUsers,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("getuserbyid")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete]
        [Route("deleteuser")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        [Route("createuser")]
        public async Task<IActionResult> Create([FromBody] UserAdd useradd)
        {

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = useradd.UserName,
                    Email = useradd.Email,
                    Phonenumber = useradd.Phonenumber,
                    Password = useradd.Password,
                    Role = useradd.Role
                };
                user.Avatar = "default.png";
                user.Status = "Active";
                user.Bio = "This is a bio";
                user.Address = "This is an address";
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("edituser")]
        public async Task<IActionResult> EditUserAPI([FromBody] UserEdit userEdit)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userEdit.UserId);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = userEdit.UserName;
            user.Email = userEdit.Email;
            user.Phonenumber = userEdit.Phonenumber;
            user.Password = userEdit.Password;
            user.Role = userEdit.Role;
            user.Address = userEdit.Address;
            user.Avatar = userEdit.Avatar;
            user.Bio = userEdit.Bio;
            user.Status = userEdit.Status;
            user.UpdatedAt = DateTime.UtcNow;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("getcategory")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _db.Categories
                                      .Include(c => c.Campaigns)
                                      .Include(c => c.Influencers)
                                      .Include(c => c.Brands)
                                      .ToListAsync();
            return Ok(categories);
        }
        [HttpDelete]
        [Route("deletecategory")]
        public async Task<IActionResult> DeleteCate(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return Ok();
        }


    }
}
