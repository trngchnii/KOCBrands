

namespace api.DTOs.Campaign
{
    public class CampaignEdit
    {
        public int CampaignId { get; set; }
        public int BrandId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public int Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

    }
}
