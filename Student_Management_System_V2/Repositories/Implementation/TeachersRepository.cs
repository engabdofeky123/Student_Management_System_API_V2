using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student_Management_System_V2.Data;
using Student_Management_System_V2.DTOs.Teacher;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Identity;
using Student_Management_System_V2.Models.Profiles;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Repositories.Implementation
{
    public class TeachersRepository : ITeachersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeachersRepository(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _userManager = manager;
        }

        public async Task<Teacher> CreateNewTeacherAsync(CreateTeacherDto dto)
        { 
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if(user == null)
                return null;
            var teacher = new Teacher
            {
                Specialization = dto.Specialization,
                UserId = dto.UserId,
            };
            var result = await _userManager.AddToRoleAsync(user, "teacher");
            if(result.Succeeded)
            {
                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();
                return teacher;
            }
            return null ;
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var del = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if (del == null) return false;
            _context.Teachers.Remove(del);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetTeacherAsync(int id)
        {
            return await _context.Teachers.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Teacher?> UpdateTeacherAsync(int id, Teacher teacher)
        {
            var teacherFromID = await _context.Teachers
                                        .FirstOrDefaultAsync(s => s.Id == id);
            if (teacher == null)
                return null;

            teacher.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == teacher.UserId);
            teacherFromID.User.FirstName = teacher.User.FirstName;
            teacherFromID.User.LastName = teacher.User.LastName;
            teacherFromID.Specialization = teacher.Specialization;

            await _context.SaveChangesAsync();
            return teacherFromID;
        }
    }
}
