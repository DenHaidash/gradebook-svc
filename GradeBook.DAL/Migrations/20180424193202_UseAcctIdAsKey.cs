using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class UseAcctIdAsKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Students_StudentRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Teachers_TeacherRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Students_StudentRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teachers_TeacherRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Group_GroupRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Subject_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebookTeacher_Teachers_TeacherRefId",
                table: "GradebookTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Specialty_SpecialityRefId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSchedule_Group_GroupRefId",
                table: "SemesterSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSchuduleSubject_Subject_SubjectRefId",
                table: "SemesterSchuduleSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Group_GroupRefId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Subject_SubjectRefId",
                table: "TeacherSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Teachers_TeacherRefId",
                table: "TeacherSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_AccountRefId",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AccountRefId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameIndex(
                name: "IX_Group_SpecialityRefId",
                table: "Groups",
                newName: "IX_Groups_SpecialityRefId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Groups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "AccountRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "AccountRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Students_StudentRefId",
                table: "FinalGrade",
                column: "StudentRefId",
                principalTable: "Students",
                principalColumn: "AccountRefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalGrade_Teachers_TeacherRefId",
                table: "FinalGrade",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "AccountRefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Students_StudentRefId",
                table: "Grade",
                column: "StudentRefId",
                principalTable: "Students",
                principalColumn: "AccountRefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Teachers_TeacherRefId",
                table: "Grade",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "AccountRefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_Groups_GroupRefId",
                table: "Gradebooks",
                column: "GroupRefId",
                principalTable: "Groups",
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
                name: "FK_GradebookTeacher_Teachers_TeacherRefId",
                table: "GradebookTeacher",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "AccountRefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialty_SpecialityRefId",
                table: "Groups",
                column: "SpecialityRefId",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSchedule_Groups_GroupRefId",
                table: "SemesterSchedule",
                column: "GroupRefId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSchuduleSubject_Subjects_SubjectRefId",
                table: "SemesterSchuduleSubject",
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
                name: "FK_TeacherSubject_Subjects_SubjectRefId",
                table: "TeacherSubject",
                column: "SubjectRefId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_Teachers_TeacherRefId",
                table: "TeacherSubject",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "AccountRefId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Students_StudentRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_FinalGrade_Teachers_TeacherRefId",
                table: "FinalGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Students_StudentRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teachers_TeacherRefId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Groups_GroupRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Subjects_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_GradebookTeacher_Teachers_TeacherRefId",
                table: "GradebookTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialty_SpecialityRefId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSchedule_Groups_GroupRefId",
                table: "SemesterSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterSchuduleSubject_Subjects_SubjectRefId",
                table: "SemesterSchuduleSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupRefId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Subjects_SubjectRefId",
                table: "TeacherSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubject_Teachers_TeacherRefId",
                table: "TeacherSubject");

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

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_SpecialityRefId",
                table: "Group",
                newName: "IX_Group_SpecialityRefId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Teachers",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Students",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AccountRefId",
                table: "Teachers",
                column: "AccountRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AccountRefId",
                table: "Students",
                column: "AccountRefId");

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
                name: "FK_Gradebooks_Group_GroupRefId",
                table: "Gradebooks",
                column: "GroupRefId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_Subject_SubjectRefId",
                table: "Gradebooks",
                column: "SubjectRefId",
                principalTable: "Subject",
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
                name: "FK_Group_Specialty_SpecialityRefId",
                table: "Group",
                column: "SpecialityRefId",
                principalTable: "Specialty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSchedule_Group_GroupRefId",
                table: "SemesterSchedule",
                column: "GroupRefId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterSchuduleSubject_Subject_SubjectRefId",
                table: "SemesterSchuduleSubject",
                column: "SubjectRefId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Group_GroupRefId",
                table: "Students",
                column: "GroupRefId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_Subject_SubjectRefId",
                table: "TeacherSubject",
                column: "SubjectRefId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubject_Teachers_TeacherRefId",
                table: "TeacherSubject",
                column: "TeacherRefId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
