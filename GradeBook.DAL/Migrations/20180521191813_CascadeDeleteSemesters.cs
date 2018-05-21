using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class CascadeDeleteSemesters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Groups_GroupRefId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_SemestersSubjects_Semesters_SemesterRefId",
                table: "SemestersSubjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Groups_GroupRefId",
                table: "Semesters",
                column: "GroupRefId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemestersSubjects_Semesters_SemesterRefId",
                table: "SemestersSubjects",
                column: "SemesterRefId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Groups_GroupRefId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_SemestersSubjects_Semesters_SemesterRefId",
                table: "SemestersSubjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Groups_GroupRefId",
                table: "Semesters",
                column: "GroupRefId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemestersSubjects_Semesters_SemesterRefId",
                table: "SemestersSubjects",
                column: "SemesterRefId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
