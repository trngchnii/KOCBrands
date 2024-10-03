using api.DTOs;
using api.Models;

namespace api.Repository
{
    public interface ISearchKOLRepository
    {
        IEnumerable<InfluencerDto> SearchKOL(string name, string? gender, int? followersCount, decimal? bookingPrice);
    }
}
