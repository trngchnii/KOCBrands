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
        Task<(Brand?, User?)> UpdateAsync(int id,UpdateBrandUserRequestDto brandModel);
        Task<Brand?> GetByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task AddAsync(Brand brand);
        Task DeleteAsync(int id);
    }
}