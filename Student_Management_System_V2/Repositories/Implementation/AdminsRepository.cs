using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management_System_V2.Data;
using Student_Management_System_V2.Helpers;
using Student_Management_System_V2.Models.Identity;
using Student_Management_System_V2.Repositories.Interfaces;
using Student_Management_System_V2.Services.Interfaces;

namespace Student_Management_System_V2.Repositories.Implementation
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminsRepository(ApplicationDbContext context)
        {
            _context = context; 
        }


        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Admin_Statistics_Response> ViewStatistics()
        {
            var teachers = await _context.Teachers.CountAsync();
            var users = await _context.Users.CountAsync();
            var students = await _context.Students.CountAsync();
            var courses = await _context.Students.CountAsync();
            var classes = await _context.Classes.CountAsync();

            return new Admin_Statistics_Response
            {
                NumberOfTeachers = teachers,
                NumberOfClasses = classes,
                NumberOfCourses = courses,
                NumberOfStudents = students,
            };
        }

       
    }
}
