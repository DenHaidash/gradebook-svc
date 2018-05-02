using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class GradebookForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Groups_GroupRefId",
                table: "Gradebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Subjects_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropIndex(
                name: "IX_Gradebooks_GroupRefId",
                table: "Gradebooks");

            migrationBuilder.DropIndex(
                name: "IX_Gradebooks_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.RenameColumn(
                name: "GroupRefId",
                table: "Gradebooks",
                newName: "SemesterRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                principalTable: "SemesterSubject",
                principalColumns: new[] { "SubjectRefId", "SemesterRefId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.RenameColumn(
                name: "SemesterRefId",
                table: "Gradebooks",
                newName: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_GroupRefId",
                table: "Gradebooks",
                column: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_SubjectRefId",
                table: "Gradebooks",
                column: "SubjectRefId");

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
        }
    }
}
