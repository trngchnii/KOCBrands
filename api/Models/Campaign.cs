using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
	public class Campaign
	{
		public int CampaignId { get; set; }
		public int? BrandId { get; set; }
		public int? FaviconId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Requirements { get; set; }
		public int Budget { get; set; }
		public DateTime StartDate { get; set; } = DateTime.Now;
		public DateTime EndDate { get; set; }
		public bool Status { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime? UpdatedDate { get; set; } = DateTime.Now;
		public Brand? Brand { get; set; }
		public List<Category>? Categories { get; set; } = new List<Category>();
		public List<Proposal>? Proposals { get; set; } = new List<Proposal>();
		public Favourite? Favourite { get; set; }
	}
}