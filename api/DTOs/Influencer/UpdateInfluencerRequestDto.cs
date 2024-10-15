using api.Models;

namespace api.DTOs
{
    public class UpdateInfluencerRequestDto
    {
        public InfluencerDto? Influencer { get; set; }
        public UserDto? User { get; set; }
    }
    public class InfluencerDto
    {
        public string Name { get; set; } = string.Empty;
        public int FollowersCount { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string BookingPrice { get; set; } = string.Empty;
        public int PersonalIdentificationNumber { get; set; }
        public List<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
        public UserDto? User { get; set; }
    }

}