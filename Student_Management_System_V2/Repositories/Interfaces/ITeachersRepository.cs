using Student_Management_System_V2.DTOs.Student;
using Student_Management_System_V2.DTOs.Teacher;
using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Repositories.Interfaces
{
    public interface ITeachersRepository
    {
        public Task<List<Teacher>> GetAllTeachersAsync(); 
        public Task<Teacher?> GetTeacherAsync(int id); 
        public Task<Teacher> CreateNewTeacherAsync(CreateTeacherDto dto); 
        public Task<Teacher?> UpdateTeacherAsync(int id, Teacher teacher);
        public Task<bool> DeleteTeacherAsync(int id); 
    }
}
