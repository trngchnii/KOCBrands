using api.Models;

namespace WebClient.Models
{
    public class BrandProfileVM
    {
        public Brand Brand { get; set; }
        public List<Campaign> Campaigns { get; set; }  

    }
}
