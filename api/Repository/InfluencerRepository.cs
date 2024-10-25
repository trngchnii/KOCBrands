using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
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

        public async Task AddAsync(Influencer influencer)
        {
            _context.Influencers.Add(influencer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var influencer = await GetByIdAsync(id);
            if (influencer != null)
            {
                _context.Influencers.Remove(influencer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Influencer>> GetAllAsync()
        {
            return await _context.Influencers.ToListAsync();
        }

        public async Task<Influencer?> GetByIdAsync(int id)
        {
            var influencer = await _context.Influencers.FirstOrDefaultAsync(i => i.InfluencerId == id);
            if (influencer == null)
                return null;
            return influencer;
        }

        public async Task<(Influencer?, User?)> UpdateAsync(int id,UpdateInfluencerRequestDto influencerModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Find the existing influencer by its ID
                var existingInfluencer = await _context.Influencers
                    .Include(b => b.User)  // Ensure User is included
                    .FirstOrDefaultAsync(b => b.InfluencerId == id);

                if (existingInfluencer == null)
                {
                    return (null, null);
                }/*
                if (existingInfluencer.User == null)
                {
                    // Handle case when User is null (either throw an error or create a new user)
                    throw new Exception("User không tồn tại cho Influencer này.");
                }*/

                // Update influencer properties
                existingInfluencer.Name = influencerModel.Influencer.Name;
                existingInfluencer.Gender = influencerModel.Influencer.Gender;
                existingInfluencer.DateOfBirth = influencerModel.Influencer.DateOfBirth;
                existingInfluencer.BookingPrice = influencerModel.Influencer.BookingPrice;
                existingInfluencer.PersonalIdentificationNumber = influencerModel.Influencer.PersonalIdentificationNumber;

                if (existingInfluencer.User != null)
                {
                    existingInfluencer.User.Email = influencerModel.User.Email;
                    existingInfluencer.User.Avatar = influencerModel.User.Avatar;
                    existingInfluencer.User.Bio = influencerModel.User.Bio;
                    existingInfluencer.User.Phonenumber = influencerModel.User.Phonenumber;
                    existingInfluencer.User.Address = influencerModel.User.Address;
                }

                // Save changes to both influencer and User
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