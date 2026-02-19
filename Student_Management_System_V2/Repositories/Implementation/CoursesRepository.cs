using Microsoft.EntityFrameworkCore;
using Student_Management_System_V2.Data;
using Student_Management_System_V2.DTOs.Course;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Profiles;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Repositories.Implementation
{
    public class CoursesRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CoursesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AssignmentResult> AssignCoursToTeacherAsync(int teacherId, int courseId)
        {
            var teacher = await _context.Teachers.Include(x=> x.Courses).FirstOrDefaultAsync(x=> x.Id == teacherId);

            if (teacher == null)
                return new AssignmentResult { Message = "Teacher not found" };

            var courseEntity = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (courseEntity == null)
                return new AssignmentResult { Message = "Course not found" };

            var alreadyAssigned = await _context.Courses.AnyAsync(sc =>
                sc.TeacherId == teacherId && sc.Id == courseId);

            if (alreadyAssigned)
                return new AssignmentResult
                {
                    Message = "Teacher already assigned to this course"
                };

            courseEntity.TeacherId = teacherId;
            teacher.Courses.Add(courseEntity);

            await _context.SaveChangesAsync();

            return new AssignmentResult
            {
                Message = "Assigned Successfully",
                IsAssigned = true
            };

        }

        public async Task<Course> CreateNewCourseAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                Description = dto.Description,
                TeacherId = dto.TeacherId
            };
            await _context.Courses.AddAsync(course); 
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var del = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (del == null) return false;
            _context.Courses.Remove(del);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetCourseAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Course> UpdateCourseAsync(int id, Course UpdatedCourse)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(s => s.Id == id);
            if (course == null) return null;

            course.Name = UpdatedCourse.Name;
            course.Description = UpdatedCourse.Description;
            course.TeacherId = UpdatedCourse.TeacherId;

            await _context.SaveChangesAsync();
            return course;
        }
    }
}
