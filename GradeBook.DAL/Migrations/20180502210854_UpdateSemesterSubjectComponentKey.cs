using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class UpdateSemesterSubjectComponentKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SemesterSubject",
                table: "SemesterSubject");

            migrationBuilder.DropIndex(
                name: "IX_SemesterSubject_SemesterRefId",
                table: "SemesterSubject");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SemesterSubject",
                table: "SemesterSubject",
                columns: new[] { "SemesterRefId", "SubjectRefId" });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubject_SubjectRefId",
                table: "SemesterSubject",
                column: "SubjectRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                principalTable: "SemesterSubject",
                principalColumns: new[] { "SemesterRefId", "SubjectRefId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SemesterSubject",
                table: "SemesterSubject");

            migrationBuilder.DropIndex(
                name: "IX_SemesterSubject_SubjectRefId",
                table: "SemesterSubject");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SemesterSubject",
                table: "SemesterSubject",
                columns: new[] { "SubjectRefId", "SemesterRefId" });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubject_SemesterRefId",
                table: "SemesterSubject",
                column: "SemesterRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_SemesterSubject_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                principalTable: "SemesterSubject",
                principalColumns: new[] { "SubjectRefId", "SemesterRefId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
