namespace Student_Management_System_V2.Helpers
{
    public class MarkAttendanceDto
    {
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
    }
}
