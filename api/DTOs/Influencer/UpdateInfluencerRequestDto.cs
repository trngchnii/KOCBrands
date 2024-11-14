using api.DTOs.SocialMedia;
using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class UpdateInfluencerRequestDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        public string? Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Range(0,double.MaxValue,ErrorMessage = "Booking Price must be a positive number.")]
        public decimal BookingPrice { get; set; }

        [Required(ErrorMessage = "Personal Identification Number is required.")]
        public int PersonalIdentificationNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        public string? Bio { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        public IFormFile? AvatarFile { get; set; }

        public string? RealAvatar { get; set; }
    }


    public class InfluencerDto
    {
        public int InfluencerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public decimal BookingPrice { get; set; }
        public int PersonalIdentificationNumber { get; set; }
        public List<SocialMediaDto>? SocialMedias { get; set; } = new List<SocialMediaDto>();

        // User sẽ có thể null nếu không cập nhật trong request này
        public UserDto? User { get; set; }
    }

    public class InfluencerEditViewModel
    {
        public Influencer Influencer { get; set; } // Dùng cho GET
        public UpdateInfluencerRequestDto InfluencerEditDto { get; set; } // Dùng cho POST
    }


}