using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
	public class ProposalRepository : IProposalRepository
	{
		private readonly ApplicationDbContext _context;
        private readonly ILogger<ProposalRepository> _logger;

        public ProposalRepository(ApplicationDbContext context,ILogger<ProposalRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Proposal proposal)
        {
            try
            {
                _context.Proposals.Add(proposal);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log error if any exception occurs
                _logger.LogError($"An error occurred while saving the proposal: {ex.Message}");
                throw;
            }
        }


        public async Task DeleteAsync(int id)
		{
			var proposal = await GetByIdAsync(id);
			if (proposal != null)
			{
				_context.Proposals.Remove(proposal);
				 await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Proposal>> GetAllAsync()
		{
			return await _context.Proposals.Include(p=>p.Influencer).ToListAsync();
		}

		public async Task<Proposal> GetByIdAsync(int id)
		{
			return await _context.Proposals.FirstOrDefaultAsync(p => p.ProposalId == id);
		}

		public async Task UpdateAsync(Proposal proposal)
		{
			var existingItem = await GetByIdAsync(proposal.ProposalId);
			if (existingItem != null)
			{
				_context.Entry(existingItem).CurrentValues.SetValues(proposal);
			}
			else
			{
				_context.Proposals.Add(proposal);
			}

			await _context.SaveChangesAsync();
		}
	}
}
