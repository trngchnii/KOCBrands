using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class InfluencerRepository : IInfluencerRepository
    {
        private readonly ApplicationDbContext _context;
        public InfluencerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<(Influencer?, User?)> UpdateAsync(int id, Influencer influencerModel, User userModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Find the existing Brand by its ID
                var existingInfluencer = await _context.Influencers
                    .Include(b => b.User)  // Ensure User is included
                    .FirstOrDefaultAsync(b => b.InfluencerId == id);

                if (existingInfluencer == null)
                {
                    return (null, null);
                }
                if (existingInfluencer.User == null)
                {
                    // Handle case when User is null (either throw an error or create a new user)
                    throw new Exception("User không tồn tại cho Brand này.");
                }

                // Update Brand properties
                existingInfluencer.Name = influencerModel.Name;
                existingInfluencer.Gender = influencerModel.Gender;
                existingInfluencer.DateOfBirth = influencerModel.DateOfBirth;
                existingInfluencer.BookingPrice = influencerModel.BookingPrice;
                existingInfluencer.PersonalIdentificationNumber = influencerModel.PersonalIdentificationNumber;

                // Update User properties
                existingInfluencer.User.Email = userModel.Email;
                existingInfluencer.User.Avatar = userModel.Avatar;
                existingInfluencer.User.Bio = userModel.Bio;
                existingInfluencer.User.Phonenumber = userModel.Phonenumber;
                existingInfluencer.User.Address = userModel.Address;

                // Save changes to both Brand and User
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (existingInfluencer, existingInfluencer.User);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}