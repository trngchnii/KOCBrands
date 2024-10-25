using api.Models;

namespace api.DTOs.CategoryDTO
{
    public class CategoryList
    {
        IEnumerable<Category> Categories { get; set; }
    }
}
