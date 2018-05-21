using Microsoft.EntityFrameworkCore;
using GradeBook.Models;

namespace GradeBook.DAL
{
    public class GradebookContext : DbContext
    {
        public GradebookContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Account>()
                .HasIndex(i => i.Login)
                .IsUnique();

            modelBuilder.Entity<Teacher>()
                .HasOne(i => i.Account)
                .WithOne(i => i.Teacher)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Student>()
                .HasOne(i => i.Account)
                .WithOne(i => i.Student)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Student>()
                .HasOne(i => i.Group)
                .WithMany(i => i.Students)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<GradebookTeacher>()
                .HasKey(i => new { i.GradebookRefId, i.TeacherRefId });
            modelBuilder.Entity<GradebookTeacher>()
                .HasOne(i => i.Gradebook)
                .WithMany(i => i.GradebookTeachers)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<GradebookTeacher>()
                .HasOne(i => i.Teacher)
                .WithMany(i => i.GradebookTeachers)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<SemesterSubject>()
                .HasKey(i => new { i.SemesterRefId, i.SubjectRefId });
            modelBuilder.Entity<SemesterSubject>()
                .HasOne(i => i.Semester)
                .WithMany(i => i.SemesterSubjects)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SemesterSubject>()
                .HasOne(i => i.Subject)
                .WithMany(i => i.Semesters)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SemesterSubject>()
                .HasOne(i => i.AssestmentType)
                .WithMany(i => i.Semesters)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FinalGrade>()
                .HasIndex(i => new {i.GradebookRefId, i.StudentRefId})
                .IsUnique();
            modelBuilder.Entity<FinalGrade>()
                .HasOne(i => i.Gradebook)
                .WithMany(i => i.FinalGrades)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FinalGrade>()
                .HasOne(i => i.Student)
                .WithMany(i => i.FinalGrades)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FinalGrade>()
                .HasOne(i => i.Teacher)
                .WithMany(i => i.FinalGrades)
                .OnDelete(DeleteBehavior.Restrict); 
                
            modelBuilder.Entity<Grade>()
                .HasOne(i => i.Gradebook)
                .WithMany(i => i.Grades)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Grade>()
                .HasOne(i => i.Student)
                .WithMany(i => i.Grades)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Grade>()
                .HasOne(i => i.Teacher)
                .WithMany(i => i.Grades)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Gradebook>()
                .HasIndex(i => new {i.SemesterRefId, i.SubjectRefId})
                .IsUnique();

            modelBuilder.Entity<GradebookTeacher>()
                .HasIndex(i => new {i.GradebookRefId, i.TeacherRefId})
                .IsUnique();
            modelBuilder.Entity<GradebookTeacher>()
                .HasOne(i => i.Gradebook)
                .WithMany(i => i.GradebookTeachers)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<GradebookTeacher>()
                .HasOne(i => i.Teacher)
                .WithMany(i => i.GradebookTeachers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Group>()
                .HasIndex(i => i.Code)
                .IsUnique();
            modelBuilder.Entity<Group>()
                .HasOne(i => i.Speciality)
                .WithMany(i => i.Groups)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Semester>()
                .HasIndex(i => new {i.GroupRefId, i.CourseNumber, i.SemesterNumber})
                .IsUnique();
            modelBuilder.Entity<Semester>()
                .HasOne(i => i.Group)
                .WithMany(i => i.Semesters)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<SemesterSubject>()
                .HasIndex(i => new {i.SemesterRefId, i.SubjectRefId})
                .IsUnique();
            modelBuilder.Entity<SemesterSubject>()
                .HasOne(i => i.AssestmentType)
                .WithMany(i => i.Semesters)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SemesterSubject>()
                .HasOne(i => i.Semester)
                .WithMany(i => i.SemesterSubjects)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SemesterSubject>()
                .HasOne(i => i.Subject)
                .WithMany(i => i.Semesters)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Specialty>()
                .HasIndex(i => i.Code)
                .IsUnique();

            modelBuilder.Entity<Specialty>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<Subject>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<Gradebook>()
                .HasOne(i => i.Semester)
                .WithMany(i => i.Gradebooks)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Gradebook>()
                .HasOne(i => i.SemesterSubject)
                .WithMany(i => i.Gradebooks)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Gradebook>()
                .HasOne(i => i.Subject)
                .WithMany(i => i.Gradebooks)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}