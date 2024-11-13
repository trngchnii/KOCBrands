using api.Data;
using api.DTOs;
using api.DTOs.Campaign;
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
            return await _context.Campaigns.Include(c => c.Brand)            
                                            .ThenInclude(b => b.User)
                                            .Include(c => c.Categories).ToListAsync();  
        }

        public async Task<Campaign> GetByIdAsync(int id)
        {
            return await _context.Campaigns.Include(c => c.Brand)
                                            .ThenInclude(b => b.User)
                                            .Include(c => c.Categories).FirstOrDefaultAsync(c => c.CampaignId == id);
        }

        public async Task<Campaign> UpdateAsync(int id, UpdateCampaignDto model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                var existingCampaign = await _context.Campaigns
                    .Include(b => b.Proposals)
                    .Include(b => b.Categories)
                    .Include(b => b.Brand)
                    .Include(b => b.Favourite)
                    .FirstOrDefaultAsync(b => b.CampaignId == id);

                if (existingCampaign == null)
                {
                    return (null);
                }

                existingCampaign.Title = model.Title;
                existingCampaign.Description = model.Description;
                existingCampaign.Requirements = model.Requirements;
                existingCampaign.Budget = model.Budget;
                existingCampaign.StartDate = model.StartDate;
                existingCampaign.EndDate = model.EndDate;
                existingCampaign.Status = model.Status;
                existingCampaign.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (existingCampaign);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
