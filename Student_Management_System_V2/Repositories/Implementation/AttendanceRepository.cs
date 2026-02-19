using Microsoft.EntityFrameworkCore;
using Student_Management_System_V2.Data;
using Student_Management_System_V2.Helpers;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Profiles;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Repositories.Implementation
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttendanceHistoryResult>> GetAttendanceHistory(int studentID)
        { 
            var partOne = _context.Attendances.Where(a => a.StudentId == studentID);
                return await partOne.Select(a => new AttendanceHistoryResult
                {
                    CourseId = a.CourseId,
                    Date = a.Date,
                    Status = a.Status,
                }).ToListAsync();   
        }

        public async Task<List<GetCourseAttendance>> GetCourseAttendances(int courseID)
        {
            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == courseID);

            if (!courseExists)
                return new List<GetCourseAttendance>();

            return await _context.Attendances.Where(a => a.CourseId == courseID).Select(a => new GetCourseAttendance
            {
                CourseId = a.CourseId,
                Date = a.Date,
                Status = a.Status,
                StudentId = a.StudentId,
                TeacherId = a.TeacherId
            }).ToListAsync();
        }

        public async Task<AttendanceResult> MarkStudentAttendance(MarkAttendanceDto dto)
        {
            var today = dto.Date.Date;

            // Check student exists
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == dto.StudentId);

            if (!studentExists)
                return new AttendanceResult { Message = "Student not found" };

            // Check course exists
            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == dto.CourseId);

            if (!courseExists)
                return new AttendanceResult { Message = "Course not found" };

            // Prevent duplicate attendance
            var existing = await _context.Attendances
                .FirstOrDefaultAsync(a => a.StudentId == dto.StudentId && a.CourseId == dto.CourseId && a.Date == today);

            if (existing != null)
            {
                return new AttendanceResult { Message = "Attendance already marked for this student today" };
            }

            var record = new Attendance
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Date = today,
                Status = dto.Status,
                TeacherId = dto.TeacherId
            };

            await _context.Attendances.AddAsync(record);
            await _context.SaveChangesAsync();

            return new AttendanceResult
            {
                IsSuccess = true,
                Message = "Attendance marked successfully"
            };
        }
    }
}
