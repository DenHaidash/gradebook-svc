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
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Login = table.Column<string>(maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 20, nullable: true),
                    PasswordHash = table.Column<string>(nullable: false),
                    PasswordSalt = table.Column<string>(nullable: false),
                    Role = table.Column<string>(maxLength: 20, nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssestmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssestmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    SpecialityRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Specialties_SpecialityRefId",
                        column: x => x.SpecialityRefId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CourseNumber = table.Column<int>(nullable: false),
                    EndsAt = table.Column<DateTime>(nullable: false),
                    GroupRefId = table.Column<int>(nullable: false),
                    SemesterNumber = table.Column<int>(nullable: false),
                    StartsAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_Groups_GroupRefId",
                        column: x => x.GroupRefId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    GroupRefId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupRefId",
                        column: x => x.GroupRefId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemestersSubjects",
                columns: table => new
                {
                    SemesterRefId = table.Column<int>(nullable: false),
                    SubjectRefId = table.Column<int>(nullable: false),
                    AssestmentTypeRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemestersSubjects", x => new { x.SemesterRefId, x.SubjectRefId });
                    table.ForeignKey(
                        name: "FK_SemestersSubjects_AssestmentTypes_AssestmentTypeRefId",
                        column: x => x.AssestmentTypeRefId,
                        principalTable: "AssestmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SemestersSubjects_Semesters_SemesterRefId",
                        column: x => x.SemesterRefId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemestersSubjects_Subjects_SubjectRefId",
                        column: x => x.SubjectRefId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gradebooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SemesterRefId = table.Column<int>(nullable: false),
                    SubjectRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gradebooks_Semesters_SemesterRefId",
                        column: x => x.SemesterRefId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gradebooks_Subjects_SubjectRefId",
                        column: x => x.SubjectRefId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gradebooks_SemestersSubjects_SemesterRefId_SubjectRefId",
                        columns: x => new { x.SemesterRefId, x.SubjectRefId },
                        principalTable: "SemestersSubjects",
                        principalColumns: new[] { "SemesterRefId", "SubjectRefId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinalGrades",
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
                    table.PrimaryKey("PK_FinalGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalGrades_Gradebooks_GradebookRefId",
                        column: x => x.GradebookRefId,
                        principalTable: "Gradebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalGrades_Students_StudentRefId",
                        column: x => x.StudentRefId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinalGrades_Teachers_TeacherRefId",
                        column: x => x.TeacherRefId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradebooksTeachers",
                columns: table => new
                {
                    GradebookRefId = table.Column<int>(nullable: false),
                    TeacherRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradebooksTeachers", x => new { x.GradebookRefId, x.TeacherRefId });
                    table.ForeignKey(
                        name: "FK_GradebooksTeachers_Gradebooks_GradebookRefId",
                        column: x => x.GradebookRefId,
                        principalTable: "Gradebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GradebooksTeachers_Teachers_TeacherRefId",
                        column: x => x.TeacherRefId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    GradebookRefId = table.Column<int>(nullable: false),
                    StudentRefId = table.Column<int>(nullable: false),
                    TeacherRefId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Gradebooks_GradebookRefId",
                        column: x => x.GradebookRefId,
                        principalTable: "Gradebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Students_StudentRefId",
                        column: x => x.StudentRefId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Teachers_TeacherRefId",
                        column: x => x.TeacherRefId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Login",
                table: "Accounts",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrades_StudentRefId",
                table: "FinalGrades",
                column: "StudentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrades_TeacherRefId",
                table: "FinalGrades",
                column: "TeacherRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalGrades_GradebookRefId_StudentRefId",
                table: "FinalGrades",
                columns: new[] { "GradebookRefId", "StudentRefId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_SubjectRefId",
                table: "Gradebooks",
                column: "SubjectRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_SemesterRefId_SubjectRefId",
                table: "Gradebooks",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GradebooksTeachers_TeacherRefId",
                table: "GradebooksTeachers",
                column: "TeacherRefId");

            migrationBuilder.CreateIndex(
                name: "IX_GradebooksTeachers_GradebookRefId_TeacherRefId",
                table: "GradebooksTeachers",
                columns: new[] { "GradebookRefId", "TeacherRefId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_GradebookRefId",
                table: "Grades",
                column: "GradebookRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentRefId",
                table: "Grades",
                column: "StudentRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TeacherRefId",
                table: "Grades",
                column: "TeacherRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Code",
                table: "Groups",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecialityRefId",
                table: "Groups",
                column: "SpecialityRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_GroupRefId_CourseNumber_SemesterNumber",
                table: "Semesters",
                columns: new[] { "GroupRefId", "CourseNumber", "SemesterNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SemestersSubjects_AssestmentTypeRefId",
                table: "SemestersSubjects",
                column: "AssestmentTypeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemestersSubjects_SubjectRefId",
                table: "SemestersSubjects",
                column: "SubjectRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemestersSubjects_SemesterRefId_SubjectRefId",
                table: "SemestersSubjects",
                columns: new[] { "SemesterRefId", "SubjectRefId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_Code",
                table: "Specialties",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_Name",
                table: "Specialties",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupRefId",
                table: "Students",
                column: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinalGrades");

            migrationBuilder.DropTable(
                name: "GradebooksTeachers");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Gradebooks");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "SemestersSubjects");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AssestmentTypes");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
