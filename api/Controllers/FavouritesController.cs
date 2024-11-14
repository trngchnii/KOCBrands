using api.Data;
using api.DTOs;
using api.DTOs.SocialMedia;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FavouritesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
        //private readonly UserManager<User> _userManager;


        public FavouritesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// Thêm vào danh sách yêu thích
		[HttpPost("add/{influencerId}")]
		public async Task<IActionResult> AddToFavorites(int influencerId)
		{
            //var user = await _userManager.GetUserAsync(User);
            //if (user == null)
            //{
            //    return Unauthorized();
            //}
            int userId = 3;

            var favorite = await _context.Favorites
										  .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.InfluencerId == influencerId);
            if (favorite != null)
            {
                return Ok(new { message = "This influencer is already in your favorites." });
            }

            var newFavorite = new Favourite
			{
				UserId = userId,
				InfluencerId = influencerId
			};

			_context.Favorites.Add(newFavorite);
			await _context.SaveChangesAsync();

			var influencer = await _context.Influencers.FirstOrDefaultAsync(i => i.InfluencerId == influencerId);
			if (influencer != null)
			{
				influencer.FavouriteId = newFavorite.FavouriteId;
				_context.Influencers.Update(influencer);
				await _context.SaveChangesAsync();
			}

            return Ok(new { message = "Added to favorites successfully!" });
        }

		// Xóa khỏi danh sách yêu thích
		[HttpDelete("remove/{influencerId}")]
		public async Task<IActionResult> RemoveFromFavorites(int influencerId)
		{
            int userId = 3;

            var favorite = await _context.Favorites
										  .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.InfluencerId == influencerId);
			if (favorite == null)
			{
				return NotFound("Influencer not found in favorites.");
			}

			_context.Favorites.Remove(favorite);
			await _context.SaveChangesAsync();

			return Ok();
		}

		// Lấy danh sách yêu thích của người dùng
		[HttpGet("list")]
		public async Task<IActionResult> GetFavorites()
		{
            int userId = 3;

            var favorites = await _context.Favorites
										   .Where(uf => uf.UserId == userId)
										   .SelectMany(uf => uf.Influencers)
                                           .Select(uf => new InfluencerDto
                                           {
                                               InfluencerId = uf.InfluencerId,
											   Name = uf.Name,
											   Gender = uf.Gender,
											   DateOfBirth = uf.DateOfBirth,
                                               BookingPrice = uf.BookingPrice,
											   PersonalIdentificationNumber = uf.PersonalIdentificationNumber,
                                               SocialMedias = uf.SocialMedias.Select(sm => new SocialMediaDto
                                               {
                                                   SocialMediaId = sm.SocialMediaId,
                                                   SocialMediaName = sm.SocialMediaName,
                                                   SocialMediaLink = sm.SocialMediaLink,
												   SocialMediaImg = sm.SocialMediaImg,
												   FollowersCount = sm.FollowersCount,
                                               }).ToList(),
                                               User = uf.User != null ? new UserDto
                                               {
                                                   Email = uf.User.Email,
                                                   Bio = uf.User.Bio,
                                                   Phonenumber = uf.User.Phonenumber,
                                                   Address = uf.User.Address,
                                                   Avatar = uf.User.Avatar
                                               } : null
                                           })
                                           .ToListAsync();

			return Ok(favorites);
		}

        [HttpGet("check/{influencerId}")]
        public async Task<IActionResult> CheckIfFavorite(int influencerId)
        {
            int userId = 3;

            // Kiểm tra nếu influencer đã có trong danh sách yêu thích
            var favorite = await _context.Favorites
                                          .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.InfluencerId == influencerId);

            // Trả về trạng thái yêu thích của influencer này (isFavorite = true hoặc false)
            return Ok(new { isFavorite = favorite != null });
        }
    }
}
