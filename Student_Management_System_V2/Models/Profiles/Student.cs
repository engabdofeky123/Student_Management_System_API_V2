using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Student_Management_System_V2.Models.Profiles
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public string FullName { get; set; }
        public int Age { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Level { get; set; }

        public int? ClassId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public Class Class { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    }
}
