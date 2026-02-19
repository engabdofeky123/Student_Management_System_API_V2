using Student_Management_System_V2.Helpers;
using Student_Management_System_V2.Models.Core;

namespace Student_Management_System_V2.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        public Task<AttendanceResult> MarkStudentAttendance(MarkAttendanceDto dto);
        public Task<List<AttendanceHistoryResult>> GetAttendanceHistory(int studentID);
        public Task<List<GetCourseAttendance>> GetCourseAttendances(int courseID);
    }
    public class GetCourseAttendance
    {
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
    }
}
