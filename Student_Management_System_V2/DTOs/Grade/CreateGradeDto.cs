namespace Student_Management_System_V2.DTOs.Grade
{
    public class CreateGradeDto
    {
        public double GradeValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}