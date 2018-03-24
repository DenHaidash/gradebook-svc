using Microsoft.EntityFrameworkCore;
using GradeBook.Models;

/*
 * dotnet ef migrations add Initial --project ../GradeBook.DAL/GradeBook.DAL.csproj

 * dotnet ef database update

 */

namespace GradeBook.DAL
{
    public class GradebookContext : DbContext
    {
        public GradebookContext(DbContextOptions<GradebookContext> options) : base(options)
        {
            
        }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Gradebook> Gradebooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GradebookTeacher>()
                .HasKey(i => new {i.GradebookRefId, i.TeacherRefId});
            
            modelBuilder.Entity<TeacherSubject>()
                .HasKey(i => new {i.SubjectRefId, i.TeacherRefId});
            
            modelBuilder.Entity<CurriculumSubject>()
                .HasKey(i => new {i.CurriculumRefId, i.SubjectRefId});
        }
    }
}