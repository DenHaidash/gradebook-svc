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
        
        // todo: check, seems redundant
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Gradebook> Gradebooks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GradebookTeacher>()
                .HasKey(i => new { i.GradebookRefId, i.TeacherRefId });
            
            modelBuilder.Entity<SemesterSubject>()
                .HasKey(i => new { i.SemesterRefId, i.SubjectRefId });
        }
    }
}