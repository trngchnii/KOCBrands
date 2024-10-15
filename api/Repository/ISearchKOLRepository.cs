using api.DTOs;
using api.Models;

namespace api.Repository
{
    public interface ISearchKOLRepository
    {
        IEnumerable<InfluencerDto> SearchKOL(string name, string? gender, DateTime? dateOfBirth, int? followersCount, decimal? bookingPrice);
    }
}
