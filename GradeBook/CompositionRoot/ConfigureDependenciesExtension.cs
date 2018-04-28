using GradeBook.Common.Mailing;
using GradeBook.DAL;
using GradeBook.DAL.Repositories;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DAL.UoW.Base;
using GradeBook.Services;
using GradeBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GradeBook.CompositionRoot
{
    public static class ConfigureDependenciesExtension
    {
        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GradebookContext");
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<GradebookContext>(options => options.UseNpgsql(connectionString));

            services.Configure<EmailSenderSettings>(configuration.GetSection("MailingSettings"));

            services.AddTransient<IEmailSender, EmailSender>();
            
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IFinalGradesRepository, FinalGradesRepository>();
            services.AddScoped<IGradebooksRepository, GradebooksRepository>();
            services.AddScoped<IGradesRepository, GradesRepository>();
            services.AddScoped<IGroupsRepository, GroupsRepository>();
            services.AddScoped<ISpecialitiesRepository, SpecialitiesRepository>();
            services.AddScoped<IStudentsRepository, StudentsRepository>();
            services.AddScoped<ISubjectsRepository, SubjectsRepository>();
            services.AddScoped<ITeachersRepository, TeachersRepository>();
            
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IGroupScheduleService, GroupScheduleService>();
            services.AddTransient<IGroupsService, GroupsService>();
            services.AddTransient<IGroupStudentsService, GroupStudentsService>();
            services.AddTransient<ISpecialitiesService, SpecialitiesService>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddTransient<IStudentGradesService, StudentGradesService>();
            services.AddTransient<IStudentsService, StudentsService>();
            services.AddTransient<ISubjectsService, SubjectsService>();
            services.AddTransient<ITeacherCoursesService, TeacherCoursesService>();
            services.AddTransient<ITeachersService, TeachersService>();
            
            services.AddScoped<IUnitOfWork<ISubjectsRepository>, UnitOfWork<ISubjectsRepository>>();
            services.AddScoped<IUnitOfWork<IAccountRepository>, UnitOfWork<IAccountRepository>>();
            services.AddScoped<IUnitOfWork<ITeachersRepository>, UnitOfWork<ITeachersRepository>>();
        }
    }
}