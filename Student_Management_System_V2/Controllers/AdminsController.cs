using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System_V2.Repositories.Interfaces;
using Student_Management_System_V2.Services.Interfaces;

namespace Student_Management_System_V2.Controllers
{
    //[Authorize("admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminsRepository _adminRepository;
        private readonly IAddRoleToUserService _addRoleService;

        public AdminsController(IAdminsRepository repo, IAddRoleToUserService service)
        {
            _adminRepository = repo;
            _addRoleService = service;
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> Get_All_Users() 
        {
            return Ok(await _adminRepository.GetAllUsers());
        }

        [HttpGet("system-stats")]
        public async Task<IActionResult> ViewStats()
        {
            var result = await _adminRepository.ViewStatistics();
            return Ok(result);
        }

        [HttpPost("assign-user-role")]
        public async Task<IActionResult> AssignUserToRole(string userId , string role)
        {
            var result = await _addRoleService.AssignRoleToUser(userId, role);
            if(result.IsAssigned)
                return Ok(result);
            return BadRequest(result);
        }

    }
}