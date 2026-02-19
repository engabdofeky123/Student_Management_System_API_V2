using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System_V2.DTOs.Teacher;
using Student_Management_System_V2.Models.Profiles;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Controllers
{
    // [Authorize(Roles ="admin,teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersRepository _teacherRepository;

        public TeachersController(ITeachersRepository repository)
        {
            _teacherRepository = repository;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll() => Ok(await _teacherRepository.GetAllTeachersAsync());

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherRepository.GetTeacherAsync(id);
            if(teacher == null)
                return NotFound("Invalid ID");
            return Ok(teacher);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNewTeacher(CreateTeacherDto dto)
        {
            var teacher = await _teacherRepository.CreateNewTeacherAsync(dto);
            if (teacher == null)
                return BadRequest("Something went wrong");
            return Ok(teacher);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTeacher(int id, Teacher updatedTeacher)
        {
            var result = await _teacherRepository.UpdateTeacherAsync(id, updatedTeacher);
            if (result == null)
                return NotFound("Can not find the teacher");
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var isDeleted = await _teacherRepository.DeleteTeacherAsync(id);
            if (isDeleted)
                return NoContent();
            return BadRequest("may be invalid ID");
        }
    }
}