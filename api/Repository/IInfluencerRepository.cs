using api.Models;

namespace api.Repository
{
    public interface IInfluencerRepository
    {
        Task<(Influencer?, User?)> UpdateAsync(int id, Influencer influencerModel, User userModel);
    }
}