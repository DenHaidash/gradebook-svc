using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class FixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Gradebooks_GradebookRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Students_StudentRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Teachers_TeacherRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Gradebooks_GradebookRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Students_StudentRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teachers_TeacherRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Semester_SemesterRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Subjects_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebookTeacher_Gradebooks_GradebookRefId",
                table: "GradebookTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebookTeacher_Teachers_TeacherRefId",
                table: "GradebookTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialty_SpecialityRefId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Groups_GroupRefId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_AssestmentType_AssestemtTypeRefId",
                table: "SemesterSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSubject_Subjects_SubjectRefId",
                table: "SemesterSubject");

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
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gradebooks",
                table: "Gradebooks");

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
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameTable(
                name: "Gradebooks",
                newName: "Gradebook");

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

            migrationBuilder.RenameColumn(
                name: "AssestemtTypeRefId",
                table: "SemesterSubject",
                newName: "AssestmentTypeRefId");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterSubject_AssestemtTypeRefId",
                table: "SemesterSubject",
                newName: "IX_SemesterSubject_AssestmentTypeRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_SpecialityRefId",
                table: "Group",
                newName: "IX_Group_SpecialityRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_Code",
                table: "Group",
                newName: "IX_Group_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebook",
                newName: "IX_Gradebook_SemesterRefId_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Gradebooks_SubjectRefId",
                table: "Gradebook",
                newName: "IX_Gradebook_SubjectRefId");

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
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gradebook",
                table: "Gradebook",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gradebook",
                table: "Gradebook");

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
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameTable(
                name: "Gradebook",
                newName: "Gradebooks");

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

            migrationBuilder.RenameColumn(
                name: "AssestmentTypeRefId",
                table: "SemesterSubject",
                newName: "AssestemtTypeRefId");

            migrationBuilder.RenameIndex(
                name: "IX_SemesterSubject_AssestmentTypeRefId",
                table: "SemesterSubject",
                newName: "IX_SemesterSubject_AssestemtTypeRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Group_SpecialityRefId",
                table: "Groups",
                newName: "IX_Groups_SpecialityRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Group_Code",
                table: "Groups",
                newName: "IX_Groups_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Gradebook_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                newName: "IX_Gradebooks_SemesterRefId_SubjectRefId");

            migrationBuilder.RenameIndex(
                name: "IX_Gradebook_SubjectRefId",
                table: "Gradebooks",
                newName: "IX_Gradebooks_SubjectRefId");

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
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gradebooks",
                table: "Gradebooks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Gradebooks_GradebookRefId",
                table: "FinalGrade",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Students_StudentRefId",
                table: "FinalGrade",
                column: "StudentRefId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Teachers_TeacherRefId",
                table: "FinalGrade",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Gradebooks_GradebookRefId",
                table: "Grade",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Students_StudentRefId",
                table: "Grade",
                column: "StudentRefId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Teachers_TeacherRefId",
                table: "Grade",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_Semester_SemesterRefId",
                table: "Gradebooks",
                column: "SemesterRefId",
                principalTable: "Semester",
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
                name: "FK_Gradebooks_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                principalTable: "SemesterSubject",
                principalColumns: new[] { "SemesterRefId", "SubjectRefId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradebookTeacher_Gradebooks_GradebookRefId",
                table: "GradebookTeacher",
                column: "GradebookRefId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradebookTeacher_Teachers_TeacherRefId",
                table: "GradebookTeacher",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialty_SpecialityRefId",
                table: "Groups",
                column: "SpecialityRefId",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Groups_GroupRefId",
                table: "Semester",
                column: "GroupRefId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_AssestmentType_AssestemtTypeRefId",
                table: "SemesterSubject",
                column: "AssestemtTypeRefId",
                principalTable: "AssestmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSubject_Subjects_SubjectRefId",
                table: "SemesterSubject",
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
    }
}
