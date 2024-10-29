using api.Models;

namespace api.DTOs.Campaign
{
    public class UpdateCampaignDto
    {
        
        public int? BrandId { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public int Budget { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        
    }
}
