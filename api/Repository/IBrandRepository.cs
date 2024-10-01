using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface IBrandRepository
    {
        Task<(Brand?, User?)> UpdateAsync(int id, Brand brandModel, User userModel);
        Task<Brand?> GetByIdAsync(int id);
    }
}