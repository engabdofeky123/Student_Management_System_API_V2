using System.ComponentModel.DataAnnotations;

namespace Student_Management_System_V2.DTOs.Teacher
{
    public class CreateTeacherDto
    {
        public string UserId { get; set; }
        public string Name { get; set; } 
        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }
    }
}
