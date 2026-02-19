using Student_Management_System_V2.DTOs.Student;
using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Repositories.Interfaces
{
    public interface IStudentsRepository
    {
        public Task<List<GetAllStudentsResult>> GetAllStudentsAsync(); 
        public Task<Student> GetStudentAsync(int id);
        public Task<Student> CreateNewStudentAsync(CreateStudentDto dto);
        public Task<Student>  UpdateStudentAsync(int id ,Student student);
        public Task<bool> DeleteStudentAsync(int id);
        public Task<Student?> GetByUserIdAsync(string userId);

        // Assign To Class & Course

        public Task<AssignmentResult> AssignStudentToClassAsync(int studentId, int classId);
        public Task<AssignmentResult> AssignStudentToCourseAsync(int studentId, int courseId);  


    }
    public class AssignmentResult 
    {
        public string Message { get; set; } 
        public bool IsAssigned { get; set; } = false;
    }

    public class GetAllStudentsResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
    }
}