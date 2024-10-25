using api.Data;
using api.DTOs;
using api.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<Brand?> GetByIdAsync(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(i => i.BrandId == id);
            if (brand == null)
                return null;
            return brand;
        }

        public async Task<(Brand?, User?)> UpdateAsync(int brandId, UpdateBrandUserRequestDto brandModel)
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
                }/*
                if (existingBrand.User == null)
                {
                    // Handle case when User is null (either throw an error or create a new user)
                    throw new Exception("User không tồn tại cho Brand này.");
                }*/

                // Update Brand properties
                existingBrand.BrandName = brandModel.Brand.BrandName;
                existingBrand.ImageCover = brandModel.Brand.ImageCover;
                existingBrand.TaxCode = brandModel.Brand.TaxCode;
                /*existingBrand.CategoryId = brandModel.CategoryId;
*/
                if (existingBrand.User != null)
                {
                    existingBrand.User.Email = brandModel.User.Email;
                    existingBrand.User.Avatar = brandModel.User.Avatar;
                    existingBrand.User.Bio = brandModel.User.Bio;
                    existingBrand.User.Phonenumber = brandModel.User.Phonenumber;
                    existingBrand.User.Address = brandModel.User.Address;
                }

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

        public async Task AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await GetByIdAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

    }
}