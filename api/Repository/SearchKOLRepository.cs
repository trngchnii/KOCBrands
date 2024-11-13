using api.Data;
using api.DTOs;
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

        public async Task<IEnumerable<Influencer>> GetAllKOCs()
        {
            return await _context.Influencers.ToListAsync();
		}

		public IEnumerable<InfluencerDto> SearchKOL(string name, string? gender, DateTime? dateOfBirth, decimal? bookingPrice, int? personalIdentificationNumber)
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

            //if (followersCount.HasValue)
            //{
            //    query = query.Where(i => i.FollowersCount >= followersCount.Value);
            //}

            if (bookingPrice.HasValue)
            {
                query = query.Where(i => i.BookingPrice <= bookingPrice.Value);
            }

            if (personalIdentificationNumber.HasValue)
            {
                query = query.Where(i => i.PersonalIdentificationNumber == personalIdentificationNumber.Value);
            }

            var result = query.Select(i => new InfluencerDto
            {
                Name = i.Name,
                Gender = i.Gender,
                DateOfBirth = i.DateOfBirth,
                BookingPrice = i.BookingPrice,
                PersonalIdentificationNumber = i.PersonalIdentificationNumber,
            }).ToList();

			return result;
        }

        public async Task<Influencer?> GetByIdAsync(int id)
        {
            var influencer = await _context.Influencers.FirstOrDefaultAsync(i => i.InfluencerId == id);
            if (influencer == null)
            {
                return null;
            }

            return influencer;
        }
    }
}
