using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class UpdateBrandUserRequestDto
    {
        public string BrandName { get; set; }
        public string? ImageCover { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile AvatarFile { get; set; }
    }

    public class BrandDto
    {
        public string BrandName { get; set; }
        public string? ImageCover { get; set; }
        public string TaxCode { get; set; }
        public UserDto? User { get; set; }
    }

    public class UserDto
    {
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public string? Bio { get; set; }
        public string? Phonenumber { get; set; }
        public string? Address { get; set; }
    }
}