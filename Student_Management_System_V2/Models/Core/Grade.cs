using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Models.Core
{
    public class Grade
    {
        public int Id { get; set; }

        public double GradeValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        // Navigation
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
