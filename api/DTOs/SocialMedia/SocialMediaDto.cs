namespace api.DTOs.SocialMedia
{
    public class SocialMediaDto
    {
        public int SocialMediaId { get; set; }
        public string SocialMediaName { get; set; } = string.Empty;
        public string SocialMediaLink { get; set; } = string.Empty;
        public string SocialMediaImg { get; set; } = string.Empty;
        public int FollowersCount { get; set; }
    }
}
