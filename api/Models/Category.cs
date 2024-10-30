using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? InfluencerId { get; set; }
        public int? BrandId { get; set; }
        public int? CampaignId { get; set; }
        public List<Campaign>? Campaigns { get; set; } = new List<Campaign>();
        public List<Influencer>? Influencers { get; set; } = new List<Influencer>();
        public List<Brand>? Brands { get; set; } = new List<Brand>();
    }
}