using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System_V2.DTOs.Student;
using Student_Management_System_V2.Models.Profiles;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Controllers
{
   // [Authorize(Roles ="admin,teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsController(IStudentsRepository repo)
        {
            _studentsRepository = repo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllStudents()
        {
           return Ok( await _studentsRepository.GetAllStudentsAsync());
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var std = await _studentsRepository.GetStudentAsync(id);
            if(std is null)
                return NotFound("Invalid ID");
            return Ok(std);
        }

        [HttpPost("create")]
        public async Task<IActionResult>CreateStudent([FromBody]CreateStudentDto dto)
        {
            var result = await _studentsRepository.CreateNewStudentAsync(dto);
            if (result is null)
                return BadRequest("An Error occurred");
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateStudent( [FromQuery] int id,[FromBody] Student updated_student)
        {
            var result = await _studentsRepository.UpdateStudentAsync(id,updated_student);
            if (result is null)
                return BadRequest("An Error occurred");
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteStudent([FromQuery] int id) 
        {
            var result = await _studentsRepository.DeleteStudentAsync(id);
            if (result == false)
                return BadRequest("ID not valid");
            return Ok(result);
        }

        [HttpPost("assign-to-course")]
        public async Task<IActionResult> AssignToCourse(int student_Id,int course_Id)
        {
            var result = await _studentsRepository.AssignStudentToCourseAsync(student_Id,course_Id);
            if(!result.IsAssigned)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("assign-to-class")]
        public async Task<IActionResult> AssignToClass(int student_Id, int class_Id)
        {
            var result = await _studentsRepository.AssignStudentToClassAsync(student_Id, class_Id);
            if (!result.IsAssigned)
                return BadRequest(result.Message);
            return Ok(result);
        }

    }
}
