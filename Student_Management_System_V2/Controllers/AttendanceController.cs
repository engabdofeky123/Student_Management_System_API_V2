using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System_V2.Helpers;
using Student_Management_System_V2.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Student_Management_System_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStudentsRepository _studentRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository, IStudentsRepository repo)
        {
            _attendanceRepository = attendanceRepository;
            _studentRepository = repo;
        }

        [HttpPost("mark")]
       // [Authorize(Roles = "teacher")]
        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _attendanceRepository.MarkStudentAttendance(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("get-course-attendance")]
        //[Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetCourseAttendance([FromQuery]int courseId)
        {
            var attendances = await _attendanceRepository.GetCourseAttendances(courseId);

            if (attendances == null || !attendances.Any())
                return NotFound("No attendance records found.");

            return Ok(attendances);
        }

        [HttpGet("my-history")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetMyAttendance()
        {
            // Extract studentId from JWT
            var studentUserIdClaim = User.FindFirst("userID")?.Value;  

            if (studentUserIdClaim == null)
                return Unauthorized();
            var student = await _studentRepository.GetByUserIdAsync(studentUserIdClaim);
            
            if (student == null)
                return NotFound("Can not find the student");

            int studentId = student.Id;
            var history = await _attendanceRepository.GetAttendanceHistory(studentId);

            if (!history.Any())
                return NotFound("No attendance history found.");

            return Ok(history);
        }

        [HttpGet("student/{studentId}")]
        //[Authorize(Roles = "admin")] 
        public async Task<IActionResult> GetStudentAttendance(int studentId)
        {
            var history = await _attendanceRepository.GetAttendanceHistory(studentId);

            if (!history.Any())
                return NotFound("No attendance history found.");

            return Ok(history);
        }
    }
}