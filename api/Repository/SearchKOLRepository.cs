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

        public IEnumerable<InfluencerDto> SearchKOL(string name, string? gender, DateTime? dateOfBirth, int? followersCount, decimal? bookingPrice)
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

            if(dateOfBirth.HasValue){
                query = query.Where(i => i.DateOfBirth == dateOfBirth.Value.Date);
            }

            if (followersCount.HasValue)
            {
           //     query = query.Where(i => i.FollowersCount >= followersCount.Value);
            }

            if (bookingPrice.HasValue)
            {
                query = query.Where(i => decimal.Parse(i.BookingPrice) <= bookingPrice.Value);
            }

            /*  var result = query.Select(i => new  
              {
                  Name = i.Name,
                  Gender = i.Gender,
                  DateOfBirth = i.DateOfBirth,
                  FollowersCount = i.FollowersCount,
                  BookingPrice = i.BookingPrice,
                  PersonalIdentificationNumber = i.PersonalIdentificationNumber,
              }).ToList();*/

            var result = query.Select(i => new InfluencerDto());

			return result;
        }
    }
}
