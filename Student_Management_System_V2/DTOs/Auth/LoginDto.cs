namespace Student_Management_System_V2.DTOs.Auth
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
