using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using GradeBook.CompositionRoot;
using GradeBook.DAL;
using GradeBook.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace GradeBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            
            services.AddCors();
            services.AddResponseCompression();
            services.AddMvc();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "GradeBook API",
                    Version = "v1",
                    Description = "GradeBook service RESTful API",
                    Contact = new Contact
                    {
                        Name = "Denis Haidash",
                        Url = "https://github.com/DenHaydash"
                    }
                });

                var xmlDocsPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml");

                if (File.Exists(xmlDocsPath))
                {
                    c.IncludeXmlComments(xmlDocsPath);
                }
                
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
                });
            });

            services.AddAutoMapper();
            
            services.ConfigureDependencies(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GradeBook API v1");
            });
            
            app.UseResponseCompression();
            
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod());
            
            app.UseAuthentication();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    serviceScope.ServiceProvider.GetService<GradebookContext>().Database.Migrate();
                }
                catch (Exception ex)
                {
                    serviceScope.ServiceProvider.GetService<ILogger<Startup>>()
                        .LogError(ex, "Failed to migrate database");
                }
            }
        }
    }
}