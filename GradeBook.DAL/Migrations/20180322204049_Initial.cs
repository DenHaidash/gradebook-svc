using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    PasswordSalt = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CourseNumber = table.Column<int>(nullable: false),
                    CourseSemesterNumber = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SemesterRefId = table.Column<int>(nullable: false),
                    SpecialtyRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curriculum_Semester_SemesterRefId",
                        column: x => x.SemesterRefId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Curriculum_Specialty_SpecialtyRefId",
                        column: x => x.SpecialtyRefId,
                        principalTable: "Specialty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: false),
                    SpecialityRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_Specialty_SpecialityRefId",
                        column: x => x.SpecialityRefId,
                        principalTable: "Specialty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EndsAt = table.Column<DateTime>(nullable: false),
                    GroupRefId = table.Column<int>(nullable: false),
                    SemesteRefId = table.Column<int>(nullable: false),
                    StartsAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterSchedule_Group_GroupRefId",
                        column: x => x.GroupRefId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSchedule_Semester_SemesteRefId",
                        column: x => x.SemesteRefId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountRefId = table.Column<int>(nullable: false),
                    GroupRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Accounts_AccountRefId",
                        column: x => x.AccountRefId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Group_GroupRefId",
                        column: x => x.GroupRefId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountRefId = table.Column<int>(nullable: false),
                    GradebookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Accounts_AccountRefId",
                        column: x => x.AccountRefId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CurriculumId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    TeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Curriculum_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subject_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gradebooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CurriculumRefId = table.Column<int>(nullable: false),
                    GroupRefId = table.Column<int>(nullable: false),
                    SubjectRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gradebooks_Curriculum_CurriculumRefId",
                        column: x => x.CurriculumRefId,
                        principalTable: "Curriculum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gradebooks_Group_GroupRefId",
                        column: x => x.GroupRefId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gradebooks_Subject_SubjectRefId",
                        column: x => x.SubjectRefId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinalGrade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    GradebookRefId = table.Column<int>(nullable: false),
                    StudentRefId = table.Column<int>(nullable: false),
                    TeacherRefId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalGrade_Gradebooks_GradebookRefId",
                        column: x => x.GradebookRefId,
                        principalTable: "Gradebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinalGrade_Students_StudentRefId",
                        column: x => x.StudentRefId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinalGrade_Teachers_TeacherRefId",
                        column: x => x.TeacherRefId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    GradebookRefId = table.Column<int>(nullable: false),
                    StudentRefId = table.Column<int>(nullable: false),
                    TeacherRefId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_Gradebooks_GradebookRefId",
                        column: x => x.GradebookRefId,
                        principalTable: "Gradebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grade_Students_StudentRefId",
                        column: x => x.StudentRefId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grade_Teachers_TeacherRefId",
                        column: x => x.TeacherRefId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_SemesterRefId",
                table: "Curriculum",
                column: "SemesterRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_SpecialtyRefId",
                table: "Curriculum",
                column: "SpecialtyRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_GradebookRefId",
                table: "FinalGrade",
                column: "GradebookRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_StudentRefId",
                table: "FinalGrade",
                column: "StudentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrade_TeacherRefId",
                table: "FinalGrade",
                column: "TeacherRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_GradebookRefId",
                table: "Grade",
                column: "GradebookRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_StudentRefId",
                table: "Grade",
                column: "StudentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_TeacherRefId",
                table: "Grade",
                column: "TeacherRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_CurriculumRefId",
                table: "Gradebooks",
                column: "CurriculumRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_GroupRefId",
                table: "Gradebooks",
                column: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_SubjectRefId",
                table: "Gradebooks",
                column: "SubjectRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_SpecialityRefId",
                table: "Group",
                column: "SpecialityRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchedule_GroupRefId",
                table: "SemesterSchedule",
                column: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchedule_SemesteRefId",
                table: "SemesterSchedule",
                column: "SemesteRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AccountRefId",
                table: "Students",
                column: "AccountRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupRefId",
                table: "Students",
                column: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_CurriculumId",
                table: "Subject",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_TeacherId",
                table: "Subject",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AccountRefId",
                table: "Teachers",
                column: "AccountRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_GradebookId",
                table: "Teachers",
                column: "GradebookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Gradebooks_GradebookId",
                table: "Teachers",
                column: "GradebookId",
                principalTable: "Gradebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curriculum_Semester_SemesterRefId",
                table: "Curriculum");

            migrationBuilder.DropForeignKey(
                name: "FK_Curriculum_Specialty_SpecialtyRefId",
                table: "Curriculum");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Specialty_SpecialityRefId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Gradebooks_GradebookId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "FinalGrade");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "SemesterSchedule");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropTable(
                name: "Gradebooks");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
