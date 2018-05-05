using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class CreateDBConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Semester_GroupRefId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropIndex(
                name: "IX_FinalGrade_GradebookRefId",
                table: "FinalGrade");

            migrationBuilder.DropColumn(
                name: "EducationStartedAt",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Specialty",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Specialty",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Groups",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Grade",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AssestmentType",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Accounts",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Accounts",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialty_Code",
                table: "Specialty",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialty_Name",
                table: "Specialty",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "SemesterSubject",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semester_GroupRefId_CourseNumber_SemesterNumber",
                table: "Semester",
                columns: new[] { "GroupRefId", "CourseNumber", "SemesterNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Code",
                table: "Groups",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GradebookTeacher_GradebookRefId_TeacherRefId",
                table: "GradebookTeacher",
                columns: new[] { "GradebookRefId", "TeacherRefId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_GradebookRefId_StudentRefId",
                table: "FinalGrade",
                columns: new[] { "GradebookRefId", "StudentRefId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_Name",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Specialty_Code",
                table: "Specialty");

            migrationBuilder.DropIndex(
                name: "IX_Specialty_Name",
                table: "Specialty");

            migrationBuilder.DropIndex(
                name: "IX_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "SemesterSubject");

            migrationBuilder.DropIndex(
                name: "IX_Semester_GroupRefId_CourseNumber_SemesterNumber",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Code",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GradebookTeacher_GradebookRefId_TeacherRefId",
                table: "GradebookTeacher");

            migrationBuilder.DropIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropIndex(
                name: "IX_FinalGrade_GradebookRefId_StudentRefId",
                table: "FinalGrade");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Specialty",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Specialty",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<DateTime>(
                name: "EducationStartedAt",
                table: "Groups",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Grade",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AssestmentType",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Semester_GroupRefId",
                table: "Semester",
                column: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" });

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_GradebookRefId",
                table: "FinalGrade",
                column: "GradebookRefId");
        }
    }
}
