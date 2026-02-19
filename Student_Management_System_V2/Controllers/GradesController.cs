using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System_V2.DTOs.Grade;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradesRepository _gradeRepo;

        public GradesController(IGradesRepository gradeRepo)
        {
            _gradeRepo = gradeRepo;
        }

        // [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> AddGrade(CreateGradeDto dto)
        {
            var result = await _gradeRepo.AddGradeAsync(dto);
            if (result == null)
                return BadRequest("An Error Occurred");
            return Ok("Grade added successfully");
        }

        //[Authorize(Roles = "Teacher")]
        [HttpPut("{gradeId}")]
        public async Task<IActionResult> UpdateGrade(int gradeId, CreateGradeDto dto)
        {
            var grade = await _gradeRepo.GetGradeByIdAsync(gradeId);
            if (grade == null)
                return NotFound("Invalid ID");

            grade.GradeValue = dto.GradeValue;
            await _gradeRepo.UpdateGradeAsync(grade);

            return Ok("Grade updated successfully");
        }

        // [Authorize(Roles = "Student")]
        [HttpGet("my-grades")]
        public async Task<IActionResult> GetMyGrades(int studentId)
        {
            var grades = await _gradeRepo.GetGradesByStudentIdAsync(studentId);
            if (grades == null)
                return NotFound("Invalid ID");
            return Ok(grades);
        }
    }
}