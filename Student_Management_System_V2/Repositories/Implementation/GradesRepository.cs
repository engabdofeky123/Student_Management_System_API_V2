using Microsoft.EntityFrameworkCore;
using Student_Management_System_V2.Data;
using Student_Management_System_V2.DTOs.Grade;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Repositories.Interfaces;
using System;

namespace Student_Management_System_V2.Repositories.Implementation
{
    public class GradesRepository : IGradesRepository
    {
        private readonly ApplicationDbContext _context;

        public GradesRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Grade> AddGradeAsync(CreateGradeDto grade)
        {
            var createdGrade = new Grade
            {
                CourseId = grade.CourseId,
                CreatedAt = DateTime.Now,
                StudentId = grade.StudentId,
                GradeValue = grade.GradeValue,
            };
            await _context.Grades.AddAsync(createdGrade);
            await _context.SaveChangesAsync();
            return createdGrade;
        }

        public async Task<Grade?> GetGradeByIdAsync(int gradeId)
        {
            return await _context.Grades.Include(g => g.Student).Include(g => g.Course)
                .FirstOrDefaultAsync(g => g.Id == gradeId);
        }

        public async Task<List<GetGradesByStudentIdAsyncResult>> GetGradesByStudentIdAsync(int studentId)
        {
            return await _context.Grades.Select(x=> new GetGradesByStudentIdAsyncResult
            {
                StudentId = studentId,
                CourseId=x.CourseId,
                GradeValue=x.GradeValue,
                GradeId = x.Id
            })   .ToListAsync();
        }

        public async Task UpdateGradeAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
        }
    }
}
