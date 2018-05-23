using Microsoft.EntityFrameworkCore.Migrations;
using System;
using GradeBook.Common.Security;

namespace GradeBook.DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("AssestmentTypes", "Description", "Екзамен");
            migrationBuilder.InsertData("AssestmentTypes", "Description", "Залік");
            migrationBuilder.InsertData("AssestmentTypes", "Description", "Диференційний залік");
            
            var adminTmpPassword = "secured";
            var salt = PasswordProtector.GenerateSalt();
            var saltedPassword = PasswordProtector.SaltString(salt, adminTmpPassword);
    
            migrationBuilder.InsertData("Accounts", new []
            {
                "FirstName", 
                "LastName", 
                "MiddleName", 
                "CreatedAt", 
                "UpdatedAt",
                "Role", 
                "PasswordHash",
                "PasswordSalt",
                "IsActive", 
                "Login"
            }, new object[]
            {
                "Admin",
                "Gradebook",
                "Api",
                DateTime.Now,
                DateTime.Now,
                Roles.Admin,
                saltedPassword,
                salt,
                true,
                "admin@gradebook.com"
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE AssestmentTypes;");
            migrationBuilder.Sql("DELETE FROM Accounts WHERE Login = 'admin@gradebook.com';");
        }
    }
}
