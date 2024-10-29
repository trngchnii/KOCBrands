namespace api.DTOs.User
{
    public class UserEdit
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Avatar { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string Phonenumber { get; set; } = string.Empty;
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
