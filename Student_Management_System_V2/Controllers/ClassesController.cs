using Microsoft.AspNetCore.Mvc;
using Student_Management_System_V2.DTOs.Class;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassRepository _classRepository;

        public ClassesController(IClassRepository repository)
        {
            _classRepository = repository;
        }

        // [Authorize(Roles ="admin")]
        [HttpGet("get-all")]
        
        public async Task<IActionResult> GetAll() => Ok(await _classRepository.GetAllClassesAsync());
        
        // [Authorize(Roles ="admin")]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            var _class = await _classRepository.GetClassAsync(id);
            if (_class == null)
                return NotFound("Invalid ID");
            return Ok(_class);
        }

        // [Authorize(Roles ="admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateNewClass(CreateClassDto dto)
        {
            var _class = await _classRepository.CreateNewClassAsync(dto);
            if (_class == null)
                return BadRequest("Something went wrong");
            return Ok(_class);
        }

        // [Authorize(Roles ="admin")]
        [HttpPut("update")] 
        public async Task<IActionResult> UpdateClass(int id, Class updatedClass)
        {
            var result = await _classRepository.UpdateClassAsync(id, updatedClass);
            if (result == null)
                return NotFound("Can not find the class");
            return Ok(result);
        }

        // [Authorize(Roles ="admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteClass(int id) 
        {
            var isDeleted = await _classRepository.DeleteClassAsync(id);
            if (isDeleted)
                return NoContent();
            return BadRequest("may be invalid ID");
        }
    }
}
