using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class GradebookForeignKeyЫуегз : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_SubjectRefId",
                table: "Gradebooks",
                column: "SubjectRefId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Semester_SemesterRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Subjects_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropIndex(
                name: "IX_Gradebooks_SubjectRefId",
                table: "Gradebooks");
        }
    }
}
