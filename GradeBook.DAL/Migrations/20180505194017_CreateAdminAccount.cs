using Microsoft.EntityFrameworkCore.Migrations;
using System;
using GradeBook.Common.Security;

namespace GradeBook.DAL.Migrations
{
    public partial class CreateAdminAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.Sql("DELETE FROM Accounts WHERE Login = 'admin@gradebook.com';");
        }
    }
}
