using api.Data;
using api.DTOs;
using api.Models;
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

        public IEnumerable<InfluencerDto> SearchKOL(string name, string? gender, int? followersCount, decimal? bookingPrice)
        {
            var query = _context.Influencers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(i => i.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(gender))
            {
                query = query.Where(i => i.Gender == gender);
            }

            if (followersCount.HasValue)
            {
                query = query.Where(i => i.FollowersCount >= followersCount.Value);
            }

            if (bookingPrice.HasValue)
            {
                query = query.Where(i => decimal.Parse(i.BookingPrice) <= bookingPrice.Value);
            }

            var result = query.Select(i => new InfluencerDto
            {
                Name = i.Name,
                Gender = i.Gender,
                FollowersCount = i.FollowersCount,
                BookingPrice = i.BookingPrice,
                SocialMediaLinks = i.SocialMediaLinks,
                PersonalIdentificationNumber = i.PersonalIdentificationNumber,
            }).ToList();

            return result;
        }
    }
}
