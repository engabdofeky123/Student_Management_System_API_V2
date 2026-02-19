using Student_Management_System_V2.DTOs.Grade;
using Student_Management_System_V2.Models.Core;

namespace Student_Management_System_V2.Repositories.Interfaces
{
    public interface IGradesRepository
    {
        Task<Grade> AddGradeAsync(CreateGradeDto grade);
        Task<List<GetGradesByStudentIdAsyncResult>> GetGradesByStudentIdAsync(int studentId);
        Task<Grade?> GetGradeByIdAsync(int gradeId);
        Task UpdateGradeAsync(Grade grade);
    }
    public class GetGradesByStudentIdAsyncResult
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int GradeId { get; set; }
        public double GradeValue { get; set; }  
    }
}