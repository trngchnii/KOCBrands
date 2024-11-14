using api.Models;

namespace api.DTOs.Campaign
{
    public class CreateCampaingDTO
    {
        public int? BrandId { get; set; }
        public int? FaviconId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; } = "requirement";
        public int Budget { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

       
    }
}
