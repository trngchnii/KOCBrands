using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CampainRepository : ICampainRepository
    {
        private readonly ApplicationDbContext _context;

        public CampainRepository(ApplicationDbContext context)
        {
            _context = context;
        } 

        public async Task AddAsync(Campaign campaign)
        {
            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var campaign = await GetByIdAsync(id);
            if (campaign != null)
            {
                _context.Campaigns.Remove(campaign);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync()
        {
            return await _context.Campaigns.ToListAsync();  
        }

        public async Task<Campaign> GetByIdAsync(int id)
        {
            return await _context.Campaigns.FirstOrDefaultAsync(c => c.CampaignId == id);
        }

        public async Task UpdateAsync(Campaign campaign)
        {
            var existingItem = await GetByIdAsync(campaign.CampaignId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(campaign);
            }
            else
            {
                _context.Campaigns.Add(campaign);
            }
            await _context.SaveChangesAsync();
        }
    }
}
