using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class AddExplicitTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Gradebook_GradebookRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Student_StudentRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Teacher_TeacherRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Gradebook_GradebookRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Student_StudentRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teacher_TeacherRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebook_Semester_SemesterRefId",
                table: "Gradebook");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebook_Subject_SubjectRefId",
                table: "Gradebook");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebook_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebook");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebookTeacher_Gradebook_GradebookRefId",
                table: "GradebookTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebookTeacher_Teacher_TeacherRefId",
                table: "GradebookTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Specialty_SpecialityRefId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Group_GroupRefId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_AssestmentType_AssestmentTypeRefId",
                table: "SemesterSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_Semester_SemesterRefId",
                table: "SemesterSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_Subject_SubjectRefId",
                table: "SemesterSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Group_GroupRefId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Account_Id",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Account_Id",
                table: "Teacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialty",
                table: "Specialty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SemesterSubject",
                table: "SemesterSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semester",
                table: "Semester");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradebookTeacher",
                table: "GradebookTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gradebook",
                table: "Gradebook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grade",
                table: "Grade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinalGrade",
                table: "FinalGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssestmentType",
                table: "AssestmentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Teacher",
                newName: "Teachers");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Specialty",
                newName: "Specialties");

            migrationBuilder.RenameTable(
                name: "SemesterSubject",
                newName: "SemestersSubjects");

            migrationBuilder.RenameTable(
                name: "Semester",
                newName: "Semesters");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameTable(
                name: "GradebookTeacher",
                newName: "GradebooksTeachers");

            migrationBuilder.RenameTable(
                name: "Gradebook",
                newName: "Gradebooks");

            migrationBuilder.RenameTable(
                name: "Grade",
                newName: "Grades");

            migrationBuilder.RenameTable(
                name: "FinalGrade",
                newName: "FinalGrades");

            migrationBuilder.RenameTable(
                name: "AssestmentType",
                newName: "AssestmentTypes");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_Name",
                table: "Subjects",
                newName: "IX_Subjects_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Student_GroupRefId",
                table: "Students",
                newName: "IX_Students_GroupRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Specialty_Name",
                table: "Specialties",
                newName: "IX_Specialties_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Specialty_Code",
                table: "Specialties",
                newName: "IX_Specialties_Code");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "SemestersSubjects",
                newName: "IX_SemestersSubjects_SemesterRefId_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterSubject_SubjectRefId",
                table: "SemestersSubjects",
                newName: "IX_SemestersSubjects_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterSubject_AssestmentTypeRefId",
                table: "SemestersSubjects",
                newName: "IX_SemestersSubjects_AssestmentTypeRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Semester_GroupRefId_CourseNumber_SemesterNumber",
                table: "Semesters",
                newName: "IX_Semesters_GroupRefId_CourseNumber_SemesterNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Group_SpecialityRefId",
                table: "Groups",
                newName: "IX_Groups_SpecialityRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Group_Code",
                table: "Groups",
                newName: "IX_Groups_Code");

            migrationBuilder.RenameIndex(
                name: "IX_GradebookTeacher_GradebookRefId_TeacherRefId",
                table: "GradebooksTeachers",
                newName: "IX_GradebooksTeachers_GradebookRefId_TeacherRefId");

            migrationBuilder.RenameIndex(
                name: "IX_GradebookTeacher_TeacherRefId",
                table: "GradebooksTeachers",
                newName: "IX_GradebooksTeachers_TeacherRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Gradebook_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                newName: "IX_Gradebooks_SemesterRefId_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Gradebook_SubjectRefId",
                table: "Gradebooks",
                newName: "IX_Gradebooks_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_TeacherRefId",
                table: "Grades",
                newName: "IX_Grades_TeacherRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_StudentRefId",
                table: "Grades",
                newName: "IX_Grades_StudentRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_GradebookRefId",
                table: "Grades",
                newName: "IX_Grades_GradebookRefId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrade_GradebookRefId_StudentRefId",
                table: "FinalGrades",
                newName: "IX_FinalGrades_GradebookRefId_StudentRefId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrade_TeacherRefId",
                table: "FinalGrades",
                newName: "IX_FinalGrades_TeacherRefId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrade_StudentRefId",
                table: "FinalGrades",
                newName: "IX_FinalGrades_StudentRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SemestersSubjects",
                table: "SemestersSubjects",
                columns: new[] { "SemesterRefId", "SubjectRefId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradebooksTeachers",
                table: "GradebooksTeachers",
                columns: new[] { "GradebookRefId", "TeacherRefId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gradebooks",
                table: "Gradebooks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grades",
                table: "Grades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinalGrades",
                table: "FinalGrades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssestmentTypes",
                table: "AssestmentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

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
                name: "FK_Semesters_Groups_GroupRefId",
                table: "Semesters",
                column: "GroupRefId",
                principalTable: "Groups",
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
                name: "FK_SemestersSubjects_Semesters_SemesterRefId",
                table: "SemestersSubjects",
                column: "SemesterRefId",
                principalTable: "Semesters",
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
                name: "FK_Semesters_Groups_GroupRefId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_SemestersSubjects_AssestmentTypes_AssestmentTypeRefId",
                table: "SemestersSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SemestersSubjects_Semesters_SemesterRefId",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specialties",
                table: "Specialties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SemestersSubjects",
                table: "SemestersSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grades",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradebooksTeachers",
                table: "GradebooksTeachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gradebooks",
                table: "Gradebooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinalGrades",
                table: "FinalGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssestmentTypes",
                table: "AssestmentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Teacher");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Specialties",
                newName: "Specialty");

            migrationBuilder.RenameTable(
                name: "SemestersSubjects",
                newName: "SemesterSubject");

            migrationBuilder.RenameTable(
                name: "Semesters",
                newName: "Semester");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameTable(
                name: "Grades",
                newName: "Grade");

            migrationBuilder.RenameTable(
                name: "GradebooksTeachers",
                newName: "GradebookTeacher");

            migrationBuilder.RenameTable(
                name: "Gradebooks",
                newName: "Gradebook");

            migrationBuilder.RenameTable(
                name: "FinalGrades",
                newName: "FinalGrade");

            migrationBuilder.RenameTable(
                name: "AssestmentTypes",
                newName: "AssestmentType");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_Name",
                table: "Subject",
                newName: "IX_Subject_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GroupRefId",
                table: "Student",
                newName: "IX_Student_GroupRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Specialties_Name",
                table: "Specialty",
                newName: "IX_Specialty_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Specialties_Code",
                table: "Specialty",
                newName: "IX_Specialty_Code");

            migrationBuilder.RenameIndex(
                name: "IX_SemestersSubjects_SemesterRefId_SubjectRefId",
                table: "SemesterSubject",
                newName: "IX_SemesterSubject_SemesterRefId_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_SemestersSubjects_SubjectRefId",
                table: "SemesterSubject",
                newName: "IX_SemesterSubject_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_SemestersSubjects_AssestmentTypeRefId",
                table: "SemesterSubject",
                newName: "IX_SemesterSubject_AssestmentTypeRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Semesters_GroupRefId_CourseNumber_SemesterNumber",
                table: "Semester",
                newName: "IX_Semester_GroupRefId_CourseNumber_SemesterNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_SpecialityRefId",
                table: "Group",
                newName: "IX_Group_SpecialityRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_Code",
                table: "Group",
                newName: "IX_Group_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_TeacherRefId",
                table: "Grade",
                newName: "IX_Grade_TeacherRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_StudentRefId",
                table: "Grade",
                newName: "IX_Grade_StudentRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_GradebookRefId",
                table: "Grade",
                newName: "IX_Grade_GradebookRefId");

            migrationBuilder.RenameIndex(
                name: "IX_GradebooksTeachers_GradebookRefId_TeacherRefId",
                table: "GradebookTeacher",
                newName: "IX_GradebookTeacher_GradebookRefId_TeacherRefId");

            migrationBuilder.RenameIndex(
                name: "IX_GradebooksTeachers_TeacherRefId",
                table: "GradebookTeacher",
                newName: "IX_GradebookTeacher_TeacherRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebook",
                newName: "IX_Gradebook_SemesterRefId_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Gradebooks_SubjectRefId",
                table: "Gradebook",
                newName: "IX_Gradebook_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrades_GradebookRefId_StudentRefId",
                table: "FinalGrade",
                newName: "IX_FinalGrade_GradebookRefId_StudentRefId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrades_TeacherRefId",
                table: "FinalGrade",
                newName: "IX_FinalGrade_TeacherRefId");

            migrationBuilder.RenameIndex(
                name: "IX_FinalGrades_StudentRefId",
                table: "FinalGrade",
                newName: "IX_FinalGrade_StudentRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specialty",
                table: "Specialty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SemesterSubject",
                table: "SemesterSubject",
                columns: new[] { "SemesterRefId", "SubjectRefId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semester",
                table: "Semester",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grade",
                table: "Grade",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradebookTeacher",
                table: "GradebookTeacher",
                columns: new[] { "GradebookRefId", "TeacherRefId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gradebook",
                table: "Gradebook",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinalGrade",
                table: "FinalGrade",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssestmentType",
                table: "AssestmentType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Gradebook_GradebookRefId",
                table: "FinalGrade",
                column: "GradebookRefId",
                principalTable: "Gradebook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Student_StudentRefId",
                table: "FinalGrade",
                column: "StudentRefId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Teacher_TeacherRefId",
                table: "FinalGrade",
                column: "TeacherRefId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Gradebook_GradebookRefId",
                table: "Grade",
                column: "GradebookRefId",
                principalTable: "Gradebook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Student_StudentRefId",
                table: "Grade",
                column: "StudentRefId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Teacher_TeacherRefId",
                table: "Grade",
                column: "TeacherRefId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebook_Semester_SemesterRefId",
                table: "Gradebook",
                column: "SemesterRefId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebook_Subject_SubjectRefId",
                table: "Gradebook",
                column: "SubjectRefId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebook_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebook",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                principalTable: "SemesterSubject",
                principalColumns: new[] { "SemesterRefId", "SubjectRefId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradebookTeacher_Gradebook_GradebookRefId",
                table: "GradebookTeacher",
                column: "GradebookRefId",
                principalTable: "Gradebook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradebookTeacher_Teacher_TeacherRefId",
                table: "GradebookTeacher",
                column: "TeacherRefId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Specialty_SpecialityRefId",
                table: "Group",
                column: "SpecialityRefId",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Group_GroupRefId",
                table: "Semester",
                column: "GroupRefId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_AssestmentType_AssestmentTypeRefId",
                table: "SemesterSubject",
                column: "AssestmentTypeRefId",
                principalTable: "AssestmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_Semester_SemesterRefId",
                table: "SemesterSubject",
                column: "SemesterRefId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_Subject_SubjectRefId",
                table: "SemesterSubject",
                column: "SubjectRefId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Group_GroupRefId",
                table: "Student",
                column: "GroupRefId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Account_Id",
                table: "Student",
                column: "Id",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Account_Id",
                table: "Teacher",
                column: "Id",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
