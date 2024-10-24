using api.Models;

namespace api.Repository
{
    public interface ICampainRepository
    {
        Task<Campaign> GetByIdAsync(int id);
        Task<IEnumerable<Campaign>> GetAllAsync();
        Task AddAsync(Campaign campaign);
        Task UpdateAsync(Campaign campaign);
        Task DeleteAsync(int id);
    }
}
