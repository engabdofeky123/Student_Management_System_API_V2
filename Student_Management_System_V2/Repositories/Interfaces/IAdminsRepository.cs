using Student_Management_System_V2.Helpers;
using Student_Management_System_V2.Models.Identity;

namespace Student_Management_System_V2.Repositories.Interfaces
{
    public interface IAdminsRepository
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<Admin_Statistics_Response> ViewStatistics();
    }
}
