using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
	public class Influencer
	{
		public int InfluencerId { get; set; }
		public int? UserId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
		public string BookingPrice { get; set; }
		public int PersonalIdentificationNumber { get; set; }
		public int? FavouriteId { get; set; }
		public User? User { get; set; }
		public List<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();
		public List<Category> Categories { get; set; } = new List<Category>();
		public List<Proposal> Proposals { get; set; } = new List<Proposal>();
		public Favourite? Favourite { get; set; }
	}
}