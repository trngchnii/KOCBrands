using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
	public class User
	{
		public int UserId { get; set; }
		public int? FavoriteId { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Address { get; set; } = string.Empty;
		public string? Avatar { get; set; }
		public string? Bio { get; set; } = string.Empty;
		public string Phonenumber { get; set; } = string.Empty;
		public string? Role { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string? Status { get; set; }
		public Influencer? Influencer { get; set; }
		public Brand? Brand { get; set; }
		public Favourite? Favourite { get; set; }
	}
}