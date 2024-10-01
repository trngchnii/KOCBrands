namespace api.Models
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }

        public string SocialMediaName { get; set; } = string.Empty;
        public string SocialMediaLink { get; set; } = string.Empty;
        public string SocialMediaImg { get; set; } = string.Empty;
        public int FollowersCount { get; set; }

    }
}
