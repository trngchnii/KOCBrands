using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all brands asynchronously
        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            // Retrieve all brands including the associated User (if needed)
            return await _context.Brands
                .Include(b => b.User)  // Eager loading of related User data if applicable
                .ToListAsync();
        }

        // Get a brand by ID asynchronously
        public async Task<Brand?> GetByIdAsync(int id)
        {
            // Find the brand by its ID, including associated User
            return await _context.Brands
                .Include(b => b.User)  // Eager load related User entity
                .FirstOrDefaultAsync(b => b.BrandId == id);
        }

        // Update a brand and its associated user
        public async Task<(Brand?, User?)> UpdateAsync(int brandId, Brand brandModel, User userModel)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Find the existing Brand by its ID
                var existingBrand = await _context.Brands
                    .Include(b => b.User)  // Ensure User is included
                    .FirstOrDefaultAsync(b => b.BrandId == brandId);

                if (existingBrand == null)
                {
                    return (null, null);
                }
                if (existingBrand.User == null)
                {
                    // Handle case when User is null (either throw an error or create a new user)
                    throw new Exception("User không tồn tại cho Brand này.");
                }

                // Update Brand properties
                existingBrand.BrandName = brandModel.BrandName;
                existingBrand.ImageCover = brandModel.ImageCover;

                // Update User properties
                existingBrand.User.Email = userModel.Email;
                existingBrand.User.Avatar = userModel.Avatar;
                existingBrand.User.Bio = userModel.Bio;
                existingBrand.User.Phonenumber = userModel.Phonenumber;
                existingBrand.User.Address = userModel.Address;

                // Save changes to both Brand and User
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return (existingBrand, existingBrand.User);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
