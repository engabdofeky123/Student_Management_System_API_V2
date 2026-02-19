using Student_Management_System_V2.Models.Profiles;
using System.ComponentModel.DataAnnotations;

namespace Student_Management_System_V2.Models.Core
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int? TeacherId { get; set; }

        // Navigation
        public Teacher? Teacher { get; set; }
        public ICollection<StudentCourse>? StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<Grade>? Grades { get; set; } = new List<Grade>();
        public ICollection<Attendance>? Attendances { get; set; } = new List<Attendance>();
    }
}
