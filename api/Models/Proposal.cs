using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Proposal
    {
        public int ProposalId { get; set; }
        public int? CampaignId { get; set; }
        public int? InfluencerId { get; set; }
        public string OfferDetails { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public Campaign? Campaign { get; set; }
        public Influencer? Influencer { get; set; }
    }
}