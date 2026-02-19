namespace Student_Management_System_V2.Helpers
{
    public class AttendanceHistoryResult
    {
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }

        public int CourseId { get; set; }
    }
}
