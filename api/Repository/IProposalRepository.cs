using api.Models;

namespace api.Repository
{
	public interface IProposalRepository
	{
		Task<Proposal> GetByIdAsync(int id);
		Task<IEnumerable<Proposal>> GetAllAsync();
		Task AddAsync(Proposal proposal);
		Task UpdateAsync(Proposal proposal);
		Task DeleteAsync(int id);
	}
}
