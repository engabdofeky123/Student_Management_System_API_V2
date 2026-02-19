using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Identity;
using Student_Management_System_V2.Models.Profiles;

namespace Student_Management_System_V2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        // Core
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        // Profiles
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ================= Tables =================
            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Teacher>().ToTable("Teachers");
            builder.Entity<Class>().ToTable("Classes");
            builder.Entity<Course>().ToTable("Courses");
            builder.Entity<StudentCourse>().ToTable("StudentCourses");
            builder.Entity<Grade>().ToTable("Grades");
            builder.Entity<Attendance>().ToTable("Attendances");

            // ================= Student ↔ User =================
            builder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ================= Teacher ↔ User =================
            builder.Entity<Teacher>()
                .HasOne(t => t.User)
                .WithOne()
                .HasForeignKey<Teacher>(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ================= Class =================
            builder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Class>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            // ================= StudentCourse (M:M) =================
            builder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            // ================= Course =================
            builder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId);

            // ================= Grade =================
            builder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);

            builder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.CourseId);

            // ================= Attendance =================
            builder.Entity<Attendance>()
                .HasIndex(a => new { a.StudentId, a.CourseId, a.Date })
                .IsUnique();

            builder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId);

            builder.Entity<Attendance>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Attendances)
                .HasForeignKey(a => a.CourseId);

            builder.Entity<Attendance>()
                .HasOne(a => a.Teacher)
                .WithMany()
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
