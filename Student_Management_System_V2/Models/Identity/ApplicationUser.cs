using Microsoft.AspNetCore.Identity;

namespace Student_Management_System_V2.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
    }
}