using Microsoft.AspNetCore.Mvc;
using Student_Management_System_V2.DTOs.Course;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository repository)
        {
            _courseRepository = repository;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCourses()
        {
            return Ok(await _courseRepository.GetAllCoursesAsync());
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetCourseById([FromQuery] int id)
        {
            var crs = await _courseRepository.GetCourseAsync(id);
            if (crs is null) 
                return NotFound("Invalid ID");
            return Ok(crs);
        }

        [HttpPost("create")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
        {
            var result = await _courseRepository.CreateNewCourseAsync(dto);
            if (result is null)
                return BadRequest("An Error occurred");
            return Ok(result);
        }

        [HttpPut("update")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdateCourse([FromQuery] int id, [FromBody] Course updated_course)
        {
            var result = await _courseRepository.UpdateCourseAsync(id, updated_course);
            if (result is null)
                return BadRequest("An Error occurred");
            return Ok(result);
        }

        [HttpDelete("delete")]
        // [Authorize(Roles ="admin")]
        public async Task<IActionResult> DeleteCourse([FromQuery] int id)
        {
            var result = await _courseRepository.DeleteCourseAsync(id);
            if (result == false)
                return BadRequest("ID not valid");
            return Ok(result);
        }

        // [Authorize(Roles ="admin")]
        [HttpPost("assign-teacher-to-course")]
        public async Task<IActionResult> AssignTeacherToCourse([FromBody]int teacher_Id, [FromQuery]int course_Id) 
        {
            var result = await _courseRepository.AssignCoursToTeacherAsync(teacher_Id, course_Id);
            if (!result.IsAssigned)
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
