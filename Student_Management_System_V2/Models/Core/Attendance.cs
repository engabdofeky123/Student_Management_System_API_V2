using Student_Management_System_V2.Helpers;
using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Models.Core
{
    public class Attendance
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }

        // Navigation
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
    }
}
