using api.Data;
using api.DTOs;
using api.DTOs.SocialMedia;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace api.Repository
{
    public class SearchKOLRepository : ISearchKOLRepository
    {
        private readonly ApplicationDbContext _context;

        public SearchKOLRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<InfluencerDto> GetAllKOCs()
        {
            var influencers = _context.Influencers
                .Include(i => i.SocialMedias)
                .Include(i => i.User)
                .Select(i => new InfluencerDto
                {
                    InfluencerId = i.InfluencerId,
                    Name = i.Name,
                    Gender = i.Gender,
                    DateOfBirth = i.DateOfBirth,
                    BookingPrice = i.BookingPrice,
                    PersonalIdentificationNumber = i.PersonalIdentificationNumber,
                    SocialMedias = i.SocialMedias.Select(sm => new SocialMediaDto
                    {
                        SocialMediaId = sm.SocialMediaId,
                        SocialMediaName = sm.SocialMediaName,
                        SocialMediaLink = sm.SocialMediaLink,
                        SocialMediaImg = sm.SocialMediaImg,
                        FollowersCount = sm.FollowersCount
                    }).ToList(),
                    User = i.User != null ? new UserDto
                    {
                        Email = i.User.Email,
                        Bio = i.User.Bio,
                        Phonenumber = i.User.Phonenumber,
                        Address = i.User.Address,
                        Avatar = i.User.Avatar
                    } : null
                })
                .ToList();

            return influencers;
        }

        public IEnumerable<InfluencerDto> SearchKOL(string name, string? gender, DateTime? dateOfBirth, decimal? bookingPrice, int? personalIdentificationNumber, string? sorting)
        {
            var query = _context.Influencers
                .Include(i => i.SocialMedias)
                .Include(i => i.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(i => i.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(gender))
            {
                query = query.Where(i => i.Gender == gender);
            }

            if(dateOfBirth.HasValue){
                query = query.Where(i => i.DateOfBirth == dateOfBirth.Value.Date);
            }

            if (bookingPrice.HasValue)
            {
                query = query.Where(i => i.BookingPrice <= bookingPrice.Value);
            }

            if (personalIdentificationNumber.HasValue)
            {
                query = query.Where(i => i.PersonalIdentificationNumber == personalIdentificationNumber.Value);
            }

            //Lọc thông tin
			if (!string.IsNullOrWhiteSpace(sorting))
			{
				switch (sorting)
				{
					case "minPrice":
						query = query.OrderBy(i => i.BookingPrice);
						break;
                    //case "avgVideo":
                    //	query = query.OrderByDescending(i => i.AvgVideoRevenue);
                    //	break;
                    //case "avgLive":
                    //	query = query.OrderByDescending(i => i.AvgLiveRevenue);
                    //	break;
                    case "followers":
						query = query.OrderByDescending(i => i.SocialMedias
				                    .Select(sm => sm.FollowersCount)
				                    .FirstOrDefault());
						break;
                    case "price":
						query = query.OrderBy(i => i.BookingPrice);
						break;
					//default:
					//	query = query.OrderBy(i => i.Name); // Mặc định sắp xếp theo tên
					//	break;
				}
			}

			var result = query.Select(i => new InfluencerDto
            {
                Name = i.Name,
                Gender = i.Gender,
                DateOfBirth = i.DateOfBirth,
                BookingPrice = i.BookingPrice,
                PersonalIdentificationNumber = i.PersonalIdentificationNumber,
                SocialMedias = i.SocialMedias.Select(sm => new SocialMediaDto
                {
                    SocialMediaId = sm.SocialMediaId,
                    SocialMediaName = sm.SocialMediaName,
                    SocialMediaLink = sm.SocialMediaLink,
                    SocialMediaImg = sm.SocialMediaImg,
                    FollowersCount = sm.FollowersCount
                }).ToList(),
                User = i.User != null ? new UserDto
                {
                    Email = i.User.Email,
                    Bio = i.User.Bio,
                    Phonenumber = i.User.Phonenumber,
                    Address = i.User.Address,
                    Avatar = i.User.Avatar
                } : null
            }).ToList();

			return result;
        }

        public async Task<Influencer?> GetByIdAsync(int id)
        {
            var influencer = await _context.Influencers
             .Include(i => i.SocialMedias)
             .Include(i => i.User)
             .FirstOrDefaultAsync(i => i.InfluencerId == id);

            if (influencer == null)
            {
                return null;
            }

            return influencer;
        }
    }
}
