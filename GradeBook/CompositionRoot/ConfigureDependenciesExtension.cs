using GradeBook.Common.Mailing;
using GradeBook.Common.Options;
using GradeBook.DAL;
using GradeBook.DAL.Repositories;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Base;
using GradeBook.Helpers;
using GradeBook.Options;
using GradeBook.Services;
using GradeBook.Services.Abstactions;
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

            services.Configure<EmailSenderOptions>(configuration.GetSection("MailingSettings"));
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            services.AddSingleton<IJwtTokenHelper, JwtTokenHelper>();
            
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
            services.AddScoped<ISemestersRepository, SemestersRepository>();
            services.AddScoped<ISemesterSubjectsRepository, SemesterSubjectsRepository>();
            services.AddScoped<IAssestmentTypesRepository, AssestmentTypesRepository>();
            services.AddScoped<ITeacherGradebookRepository, TeacherGradebookRepository>();
            
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
            services.AddTransient<IGroupSemestersService, GroupSemestersService>();
            services.AddTransient<IAssestmentTypesService, AssestmentTypesService>();
            services.AddTransient<IGradebooksService, GradebookService>();
            
            // move to module
            services.AddScoped<IUnitOfWork<ISubjectsRepository>, UnitOfWork<ISubjectsRepository>>();
            services.AddScoped<IUnitOfWork<IAccountRepository>, UnitOfWork<IAccountRepository>>();
            services.AddScoped<IUnitOfWork<ITeachersRepository>, UnitOfWork<ITeachersRepository>>();
            services.AddScoped<IUnitOfWork<ISpecialitiesRepository>, UnitOfWork<ISpecialitiesRepository>>();
            services.AddScoped<IUnitOfWork<IGroupsRepository>, UnitOfWork<IGroupsRepository>>();
            services.AddScoped<IUnitOfWork<IStudentsRepository>, UnitOfWork<IStudentsRepository>>();
            services.AddScoped<IUnitOfWork<ISemestersRepository>, UnitOfWork<ISemestersRepository>>();
            services.AddScoped<IUnitOfWork<ISemesterSubjectsRepository>, UnitOfWork<ISemesterSubjectsRepository>>();
            services.AddScoped<IUnitOfWork<IGradebooksRepository>, UnitOfWork<IGradebooksRepository>>();
            services.AddScoped<IUnitOfWork<ITeacherGradebookRepository>, UnitOfWork<ITeacherGradebookRepository>>();
        }
    }
}