using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Favourite
    {
        public int FavouriteId { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public List<Influencer> Influencers { get; set; } = new List<Influencer>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Campaign> Campaigns { get; set; } = new List<Campaign>();
    }
}
