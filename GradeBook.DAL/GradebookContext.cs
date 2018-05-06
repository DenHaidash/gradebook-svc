using Microsoft.EntityFrameworkCore;
using GradeBook.Models;

/*
 * dotnet ef migrations add Initial --project ../GradeBook.DAL/GradeBook.DAL.csproj

 * dotnet ef database update
docker run -p 1111:5432 -e POSTGRES_PASSWORD=mysecretpassword -v pg_data:/var/lib/postgresql/data postgres
 */

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
            
            modelBuilder.Entity<GradebookTeacher>()
                .HasKey(i => new { i.GradebookRefId, i.TeacherRefId });
            
            modelBuilder.Entity<SemesterSubject>()
                .HasKey(i => new { i.SemesterRefId, i.SubjectRefId });

            modelBuilder.Entity<FinalGrade>()
                .HasIndex(i => new {i.GradebookRefId, i.StudentRefId})
                .IsUnique();

            modelBuilder.Entity<Gradebook>()
                .HasIndex(i => new {i.SemesterRefId, i.SubjectRefId})
                .IsUnique();

            modelBuilder.Entity<GradebookTeacher>()
                .HasIndex(i => new {i.GradebookRefId, i.TeacherRefId})
                .IsUnique();

            modelBuilder.Entity<Group>()
                .HasIndex(i => i.Code)
                .IsUnique();

            modelBuilder.Entity<Semester>()
                .HasIndex(i => new {i.GroupRefId, i.CourseNumber, i.SemesterNumber})
                .IsUnique();
            
            modelBuilder.Entity<SemesterSubject>()
                .HasIndex(i => new {i.SemesterRefId, i.SubjectRefId})
                .IsUnique();

            modelBuilder.Entity<Specialty>()
                .HasIndex(i => i.Code)
                .IsUnique();

            modelBuilder.Entity<Specialty>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<Subject>()
                .HasIndex(i => i.Name)
                .IsUnique();
        }
    }
}