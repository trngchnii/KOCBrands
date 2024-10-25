using api.DTOs;
using api.Models;

namespace api.Repository
{
    public interface IInfluencerRepository
    {
        Task<(Influencer?, User?)> UpdateAsync(int id, UpdateInfluencerRequestDto influencerModel);
        Task<Influencer?> GetByIdAsync(int id);
        Task<IEnumerable<Influencer>> GetAllAsync();
        Task AddAsync(Influencer influencer);
        Task DeleteAsync(int id);
    }
}