using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class ManyToManySetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Curriculum_CurriculumId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Teachers_TeacherId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Gradebooks_GradebookId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_GradebookId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Subject_CurriculumId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_TeacherId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "GradebookId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CurriculumId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Subject");

            migrationBuilder.CreateTable(
                name: "CurriculumSubject",
                columns: table => new
                {
                    CurriculumRefId = table.Column<int>(nullable: false),
                    SubjectRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumSubject", x => new { x.CurriculumRefId, x.SubjectRefId });
                    table.ForeignKey(
                        name: "FK_CurriculumSubject_Curriculum_CurriculumRefId",
                        column: x => x.CurriculumRefId,
                        principalTable: "Curriculum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumSubject_Subject_SubjectRefId",
                        column: x => x.SubjectRefId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradebookTeacher",
                columns: table => new
                {
                    GradebookRefId = table.Column<int>(nullable: false),
                    TeacherRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradebookTeacher", x => new { x.GradebookRefId, x.TeacherRefId });
                    table.ForeignKey(
                        name: "FK_GradebookTeacher_Gradebooks_GradebookRefId",
                        column: x => x.GradebookRefId,
                        principalTable: "Gradebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradebookTeacher_Teachers_TeacherRefId",
                        column: x => x.TeacherRefId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubject",
                columns: table => new
                {
                    SubjectRefId = table.Column<int>(nullable: false),
                    TeacherRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubject", x => new { x.SubjectRefId, x.TeacherRefId });
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Subject_SubjectRefId",
                        column: x => x.SubjectRefId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Teachers_TeacherRefId",
                        column: x => x.TeacherRefId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSubject_SubjectRefId",
                table: "CurriculumSubject",
                column: "SubjectRefId");

            migrationBuilder.CreateIndex(
                name: "IX_GradebookTeacher_TeacherRefId",
                table: "GradebookTeacher",
                column: "TeacherRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubject_TeacherRefId",
                table: "TeacherSubject",
                column: "TeacherRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurriculumSubject");

            migrationBuilder.DropTable(
                name: "GradebookTeacher");

            migrationBuilder.DropTable(
                name: "TeacherSubject");

            migrationBuilder.AddColumn<int>(
                name: "GradebookId",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurriculumId",
                table: "Subject",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Subject",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_GradebookId",
                table: "Teachers",
                column: "GradebookId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_CurriculumId",
                table: "Subject",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_TeacherId",
                table: "Subject",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Curriculum_CurriculumId",
                table: "Subject",
                column: "CurriculumId",
                principalTable: "Curriculum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Teachers_TeacherId",
                table: "Subject",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Gradebooks_GradebookId",
                table: "Teachers",
                column: "GradebookId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
