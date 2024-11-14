using api.DTOs;
using api.Models;

namespace api.Repository
{
    public interface ISearchKOLRepository
    {
        IEnumerable<InfluencerDto> SearchKOL(string name, string? gender, DateTime? dateOfBirth, decimal? bookingPrice, int? personalIdentificationNumber, string? sorting);
        IEnumerable<InfluencerDto> GetAllKOCs();
        Task<Influencer?> GetByIdAsync(int id);
    }
}
