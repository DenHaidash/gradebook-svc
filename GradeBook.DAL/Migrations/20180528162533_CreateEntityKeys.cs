using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class CreateEntityKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SemestersSubjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GradebooksTeachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SemestersSubjects_Id",
                table: "SemestersSubjects",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GradebooksTeachers_Id",
                table: "GradebooksTeachers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SemestersSubjects_Id",
                table: "SemestersSubjects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GradebooksTeachers_Id",
                table: "GradebooksTeachers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SemestersSubjects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GradebooksTeachers");
        }
    }
}
