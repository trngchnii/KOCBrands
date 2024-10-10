using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public int? UserId { get; set; }
        public string BrandName { get; set; }
        public string ImageCover { get; set; }
        public string TaxCode { get; set; }
        public User? User { get; set; }
        public List<Campaign> Campaigns { get; set; } = new List<Campaign>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}