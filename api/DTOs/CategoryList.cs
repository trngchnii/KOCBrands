using api.Models;

namespace api.DTOs
{
    public class CategoryList
    {
        IEnumerable<Category> Categories { get; set; }
    }
}
