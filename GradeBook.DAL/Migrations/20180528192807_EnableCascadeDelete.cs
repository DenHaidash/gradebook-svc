using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class EnableCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrades_Gradebooks_GradebookRefId",
                table: "FinalGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrades_Students_StudentRefId",
                table: "FinalGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrades_Teachers_TeacherRefId",
                table: "FinalGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Semesters_SemesterRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Subjects_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_SemestersSubjects_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebooksTeachers_Gradebooks_GradebookRefId",
                table: "GradebooksTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebooksTeachers_Teachers_TeacherRefId",
                table: "GradebooksTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Gradebooks_GradebookRefId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentRefId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_TeacherRefId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialties_SpecialityRefId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_SemestersSubjects_AssestmentTypes_AssestmentTypeRefId",
                table: "SemestersSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SemestersSubjects_Subjects_SubjectRefId",
                table: "SemestersSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupRefId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Accounts_Id",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Accounts_Id",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrades_Gradebooks_GradebookRefId",
                table: "FinalGrades",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrades_Students_StudentRefId",
                table: "FinalGrades",
                column: "StudentRefId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrades_Teachers_TeacherRefId",
                table: "FinalGrades",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_Semesters_SemesterRefId",
                table: "Gradebooks",
                column: "SemesterRefId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_Subjects_SubjectRefId",
                table: "Gradebooks",
                column: "SubjectRefId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_SemestersSubjects_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                principalTable: "SemestersSubjects",
                principalColumns: new[] { "SemesterRefId", "SubjectRefId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradebooksTeachers_Gradebooks_GradebookRefId",
                table: "GradebooksTeachers",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradebooksTeachers_Teachers_TeacherRefId",
                table: "GradebooksTeachers",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Gradebooks_GradebookRefId",
                table: "Grades",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentRefId",
                table: "Grades",
                column: "StudentRefId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_TeacherRefId",
                table: "Grades",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialties_SpecialityRefId",
                table: "Groups",
                column: "SpecialityRefId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemestersSubjects_AssestmentTypes_AssestmentTypeRefId",
                table: "SemestersSubjects",
                column: "AssestmentTypeRefId",
                principalTable: "AssestmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemestersSubjects_Subjects_SubjectRefId",
                table: "SemestersSubjects",
                column: "SubjectRefId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupRefId",
                table: "Students",
                column: "GroupRefId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Accounts_Id",
                table: "Students",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Accounts_Id",
                table: "Teachers",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrades_Gradebooks_GradebookRefId",
                table: "FinalGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrades_Students_StudentRefId",
                table: "FinalGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrades_Teachers_TeacherRefId",
                table: "FinalGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Semesters_SemesterRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Subjects_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_SemestersSubjects_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebooksTeachers_Gradebooks_GradebookRefId",
                table: "GradebooksTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebooksTeachers_Teachers_TeacherRefId",
                table: "GradebooksTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Gradebooks_GradebookRefId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentRefId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Teachers_TeacherRefId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialties_SpecialityRefId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_SemestersSubjects_AssestmentTypes_AssestmentTypeRefId",
                table: "SemestersSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SemestersSubjects_Subjects_SubjectRefId",
                table: "SemestersSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupRefId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Accounts_Id",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Accounts_Id",
                table: "Teachers");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teachers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Accounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrades_Gradebooks_GradebookRefId",
                table: "FinalGrades",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrades_Students_StudentRefId",
                table: "FinalGrades",
                column: "StudentRefId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrades_Teachers_TeacherRefId",
                table: "FinalGrades",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_Semesters_SemesterRefId",
                table: "Gradebooks",
                column: "SemesterRefId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_Subjects_SubjectRefId",
                table: "Gradebooks",
                column: "SubjectRefId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_SemestersSubjects_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                principalTable: "SemestersSubjects",
                principalColumns: new[] { "SemesterRefId", "SubjectRefId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradebooksTeachers_Gradebooks_GradebookRefId",
                table: "GradebooksTeachers",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GradebooksTeachers_Teachers_TeacherRefId",
                table: "GradebooksTeachers",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Gradebooks_GradebookRefId",
                table: "Grades",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentRefId",
                table: "Grades",
                column: "StudentRefId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Teachers_TeacherRefId",
                table: "Grades",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialties_SpecialityRefId",
                table: "Groups",
                column: "SpecialityRefId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemestersSubjects_AssestmentTypes_AssestmentTypeRefId",
                table: "SemestersSubjects",
                column: "AssestmentTypeRefId",
                principalTable: "AssestmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemestersSubjects_Subjects_SubjectRefId",
                table: "SemestersSubjects",
                column: "SubjectRefId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupRefId",
                table: "Students",
                column: "GroupRefId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Accounts_Id",
                table: "Students",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Accounts_Id",
                table: "Teachers",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
