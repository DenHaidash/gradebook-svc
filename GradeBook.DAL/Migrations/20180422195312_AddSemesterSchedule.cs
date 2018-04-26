using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class AddSemesterSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gradebooks_Curriculum_CurriculumRefId",
                table: "Gradebooks");

            migrationBuilder.DropTable(
                name: "CurriculumSubject");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropIndex(
                name: "IX_Gradebooks_CurriculumRefId",
                table: "Gradebooks");

            migrationBuilder.DropColumn(
                name: "CurriculumRefId",
                table: "Gradebooks");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teachers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Grade",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Accounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Accounts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AssestmentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssestmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SemesterSchuduleSubject",
                columns: table => new
                {
                    SubjectRefId = table.Column<int>(nullable: false),
                    SemesterScheduleRefId = table.Column<int>(nullable: false),
                    AssestemtTypeRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSchuduleSubject", x => new { x.SubjectRefId, x.SemesterScheduleRefId });
                    table.ForeignKey(
                        name: "FK_SemesterSchuduleSubject_AssestmentType_AssestemtTypeRefId",
                        column: x => x.AssestemtTypeRefId,
                        principalTable: "AssestmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSchuduleSubject_SemesterSchedule_SemesterScheduleRefId",
                        column: x => x.SemesterScheduleRefId,
                        principalTable: "SemesterSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSchuduleSubject_Subject_SubjectRefId",
                        column: x => x.SubjectRefId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchuduleSubject_AssestemtTypeRefId",
                table: "SemesterSchuduleSubject",
                column: "AssestemtTypeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchuduleSubject_SemesterScheduleRefId",
                table: "SemesterSchuduleSubject",
                column: "SemesterScheduleRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterSchuduleSubject");

            migrationBuilder.DropTable(
                name: "AssestmentType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "CurriculumRefId",
                table: "Gradebooks",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Gradebooks_CurriculumRefId",
                table: "Gradebooks",
                column: "CurriculumRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_SemesterRefId",
                table: "Curriculum",
                column: "SemesterRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_SpecialtyRefId",
                table: "Curriculum",
                column: "SpecialtyRefId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSubject_SubjectRefId",
                table: "CurriculumSubject",
                column: "SubjectRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gradebooks_Curriculum_CurriculumRefId",
                table: "Gradebooks",
                column: "CurriculumRefId",
                principalTable: "Curriculum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
