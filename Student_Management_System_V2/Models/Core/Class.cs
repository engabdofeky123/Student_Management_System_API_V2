using Student_Management_System_V2.Models.Identity;
using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Models.Core
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? TeacherId { get; set; }

        // Navigation
        public ApplicationUser? Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
