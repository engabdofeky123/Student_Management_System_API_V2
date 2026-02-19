using Student_Management_System_V2.DTOs.Auth;

namespace Student_Management_System_V2.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthModel> Register(RegisterDto dto); 
        public Task<AuthModel> LogIn(LoginDto dto);

    }
    public class AuthModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime ExporesON { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}