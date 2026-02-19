using Student_Management_System_V2.DTOs.Course;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<Course>> GetAllCoursesAsync(); 
        public Task<Course?> GetCourseAsync(int id); 
        public Task<Course> CreateNewCourseAsync(CreateCourseDto dto);
        public Task<Course> UpdateCourseAsync(int id, Course UpdatedCourse); 
        public Task<bool> DeleteCourseAsync(int id); 

        // Assign To Class & Course

        public Task<AssignmentResult> AssignCoursToTeacherAsync(int teacherId, int courseId); 
    }
}
