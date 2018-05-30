using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class FixPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SemestersSubjects_Id",
                table: "SemestersSubjects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GradebooksTeachers_Id",
                table: "GradebooksTeachers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_SemestersSubjects_Id",
                table: "SemestersSubjects",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GradebooksTeachers_Id",
                table: "GradebooksTeachers",
                column: "Id");
        }
    }
}
