using api.Models;

namespace api.Repository
{
    public interface IInfluencerRepository
    {
        Task<(Influencer?, User?)> UpdateAsync(int id, Influencer influencerModel, User userModel);
        Task<Influencer?> GetByIdAsync(int id);
        Task<IEnumerable<Influencer>> GetAllAsync();
        Task AddAsync(Influencer influencer);
        Task DeleteAsync(int id);
    }
}