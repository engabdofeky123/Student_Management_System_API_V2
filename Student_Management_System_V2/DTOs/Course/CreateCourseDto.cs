using System.ComponentModel.DataAnnotations;

namespace Student_Management_System_V2.DTOs.Course
{
    public class CreateCourseDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int? TeacherId { get; set; }
    }
}
