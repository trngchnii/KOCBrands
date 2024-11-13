using api.DTOs;
using api.DTOs.Campaign;
using api.Models;

namespace api.Repository
{
    public interface ICampainRepository
    {
        Task<Campaign> GetByIdAsync(int id);
        Task<IEnumerable<Campaign>> GetAllAsync();
        Task AddAsync(Campaign campaign);
        Task<Campaign> UpdateAsync(Campaign campaign);
        Task DeleteAsync(int id);
    }
}
