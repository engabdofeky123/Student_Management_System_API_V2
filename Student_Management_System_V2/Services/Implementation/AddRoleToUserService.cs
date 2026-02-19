using Microsoft.AspNetCore.Identity;
using Student_Management_System_V2.Models.Identity;
using Student_Management_System_V2.Repositories.Interfaces;
using Student_Management_System_V2.Services.Interfaces;
using System.Linq;

namespace Student_Management_System_V2.Services.Implementation
{
    public class AddRoleToUserService : IAddRoleToUserService
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManger;

        public AddRoleToUserService(UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _UserManager = user;
            _RoleManger = role;
        }
        public async Task<AssignmentResult> AssignRoleToUser(string userId, string newRole)
        {
            var user = await _UserManager.FindByIdAsync(userId);
            if (user == null)
                return new AssignmentResult { Message = "User Not Found" };

            var role = await _RoleManger.RoleExistsAsync(newRole);
            if (!role)
                return new AssignmentResult { Message = "Invalid roles. use only (admin/student/teacher) roles" };

            if (await _UserManager.IsInRoleAsync(user, newRole))
                return new AssignmentResult { Message = "Already assignd" };

            var result = await _UserManager.AddToRoleAsync(user, newRole);
            if (result.Succeeded)
                return new AssignmentResult { Message = "Successfully assigned", IsAssigned = true };
            return new AssignmentResult { Message = "There is an error" };

        }
    }
}
