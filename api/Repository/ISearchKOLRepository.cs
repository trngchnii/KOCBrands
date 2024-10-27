using api.DTOs;
using api.Models;

namespace api.Repository
{
    public interface ISearchKOLRepository
    {
        IEnumerable<InfluencerDto> SearchKOL(string name, string? gender, DateTime? dateOfBirth, decimal? bookingPrice, int? personalIdentificationNumber);
        Task<IEnumerable<Influencer>> GetAllKOCs();
        Task<Influencer?> GetByIdAsync(int id);
    }
}
