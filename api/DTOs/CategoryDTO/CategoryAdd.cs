using api.Models;

namespace api.DTOs.CategoryDTO
{
    public class CategoryAdd
    {
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
