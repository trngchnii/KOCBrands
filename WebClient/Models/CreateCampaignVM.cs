using api.Models;

namespace WebClient.Models
{
    public class CreateCampaignVM
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public int Budget { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public List<Category> Categories { get; set; }

        public int CategoryID { get; set; }

    }
}
