using Student_Management_System_V2.DTOs.Class;
using Student_Management_System_V2.DTOs.Teacher;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Repositories.Interfaces
{
    public interface IClassRepository
    {
        public Task<List<Class>> GetAllClassesAsync(); 
        public Task<Class?> GetClassAsync(int id); 
        public Task<Class> CreateNewClassAsync(CreateClassDto dto); 
        public Task<Class?> UpdateClassAsync(int id, Class _class);
        public Task<bool> DeleteClassAsync(int id); 
    }
}
