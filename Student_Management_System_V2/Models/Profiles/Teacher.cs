using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Student_Management_System_V2.Models.Profiles
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
