using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Services.Interfaces
{
    public interface IAddRoleToUserService
    {
        Task<AssignmentResult> AssignRoleToUser(string userId, string newRole);
    }
}
