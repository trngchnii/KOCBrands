using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;

namespace api.Repository
{
    public interface IBrandRepository
    {
        Task UpdateBrandAsync(int brandId,UpdateBrandUserRequestDto requestDto);
        Task<Brand?> GetByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task AddAsync(Brand brand);
        Task DeleteAsync(int id);
    }
}