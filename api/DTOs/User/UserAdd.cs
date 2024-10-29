namespace api.DTOs.User
{
    public class UserAdd
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; } = string.Empty;
        public string Role { get; set; }
    }
}
