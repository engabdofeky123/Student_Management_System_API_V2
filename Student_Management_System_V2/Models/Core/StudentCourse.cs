using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Models.Core
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        // Navigation
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
