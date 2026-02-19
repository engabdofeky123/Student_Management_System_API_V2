using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student_Management_System_V2.Data;
using Student_Management_System_V2.DTOs.Student;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Identity;
using Student_Management_System_V2.Models.Profiles;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Repositories.Implementation
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsRepository(ApplicationDbContext context, UserManager<ApplicationUser> manager )
        {
            _context = context;
            _userManager = manager;
        }

        public async Task<Student> CreateNewStudentAsync(CreateStudentDto dto)
        {
            var user = await _userManager.FindByIdAsync( dto.UserId );

            if(user == null)
                return null;
            var student = new Student
            {
                Age = dto.Age,
                Level = dto.Level,
                UserId = dto.UserId,
                EnrollmentDate = DateTime.UtcNow,
            };

            var result = await _userManager.AddToRoleAsync(user, "student");
            if(result.Succeeded)
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return student;
            }
            return null ;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var del = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (del == null) return false;
            _context.Students.Remove(del); 
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetAllStudentsResult>> GetAllStudentsAsync()
        {
            return await _context.Students.Select(s => new GetAllStudentsResult
            {
                Id = s.Id,
                ClassName = s.Class.Name,
                Name = s.User.FirstName + ' ' + s.User.LastName,
            }).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s=> s.Id == id);
        }

        public async Task<Student> UpdateStudentAsync(int id, Student updatedStudent)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return null;

            student.Age = updatedStudent.Age; 
            student.Level = updatedStudent.Level;
            student.ClassId = updatedStudent.ClassId;

            await _context.SaveChangesAsync();
            return student;
        }
       
        public async Task<Student?> GetByUserIdAsync(string userId)
        {
            return await _context.Students.Include(s => s.Class).FirstOrDefaultAsync(s => s.UserId == userId);
        }

        // Assign Methods

        public async Task<AssignmentResult> AssignStudentToClassAsync(int studentId, int classId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return new AssignmentResult { Message = "Student not found"};

            var classEntity = await _context.Classes.FirstOrDefaultAsync(c => c.Id == classId);

            if (classEntity == null)
                return new AssignmentResult { Message = "Class not found" };

            student.ClassId = classId;

            await _context.SaveChangesAsync();

            return new AssignmentResult { Message = "Assigned Successfully" , IsAssigned = true };

        }

        public async Task<AssignmentResult> AssignStudentToCourseAsync(int studentId, int courseId)
        {
            var student = await _context.Students
      .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return new AssignmentResult { Message = "Student not found" };

            var courseEntity = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (courseEntity == null)
                return new AssignmentResult { Message = "Course not found" };

            var alreadyAssigned = await _context.StudentCourses.AnyAsync(sc =>
                sc.StudentId == studentId && sc.CourseId == courseId);

            if (alreadyAssigned)
                return new AssignmentResult
                {
                    Message = "Student already assigned to this course"
                };

            var stdCrs = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            await _context.StudentCourses.AddAsync(stdCrs);
            await _context.SaveChangesAsync();

            return new AssignmentResult
            {
                Message = "Assigned Successfully",
                IsAssigned = true
            };
        }
    }

    
}
