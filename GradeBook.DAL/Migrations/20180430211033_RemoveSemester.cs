using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class RemoveSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterSchuduleSubject");

            migrationBuilder.DropTable(
                name: "SemesterSchedule");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Semester",
                newName: "SemesterNumber");

            migrationBuilder.RenameColumn(
                name: "CourseSemesterNumber",
                table: "Semester",
                newName: "GroupRefId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndsAt",
                table: "Semester",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartsAt",
                table: "Semester",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SemesterSubject",
                columns: table => new
                {
                    SubjectRefId = table.Column<int>(nullable: false),
                    SemesterRefId = table.Column<int>(nullable: false),
                    AssestemtTypeRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSubject", x => new { x.SubjectRefId, x.SemesterRefId });
                    table.ForeignKey(
                        name: "FK_SemesterSubject_AssestmentType_AssestemtTypeRefId",
                        column: x => x.AssestemtTypeRefId,
                        principalTable: "AssestmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSubject_Semester_SemesterRefId",
                        column: x => x.SemesterRefId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSubject_Subjects_SubjectRefId",
                        column: x => x.SubjectRefId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Semester_GroupRefId",
                table: "Semester",
                column: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubject_AssestemtTypeRefId",
                table: "SemesterSubject",
                column: "AssestemtTypeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubject_SemesterRefId",
                table: "SemesterSubject",
                column: "SemesterRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Groups_GroupRefId",
                table: "Semester",
                column: "GroupRefId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Groups_GroupRefId",
                table: "Semester");

            migrationBuilder.DropTable(
                name: "SemesterSubject");

            migrationBuilder.DropIndex(
                name: "IX_Semester_GroupRefId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "EndsAt",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "StartsAt",
                table: "Semester");

            migrationBuilder.RenameColumn(
                name: "SemesterNumber",
                table: "Semester",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "GroupRefId",
                table: "Semester",
                newName: "CourseSemesterNumber");

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
                        name: "FK_SemesterSchedule_Groups_GroupRefId",
                        column: x => x.GroupRefId,
                        principalTable: "Groups",
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
                        name: "FK_SemesterSchuduleSubject_Subjects_SubjectRefId",
                        column: x => x.SubjectRefId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchedule_GroupRefId",
                table: "SemesterSchedule",
                column: "GroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchedule_SemesteRefId",
                table: "SemesterSchedule",
                column: "SemesteRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchuduleSubject_AssestemtTypeRefId",
                table: "SemesterSchuduleSubject",
                column: "AssestemtTypeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchuduleSubject_SemesterScheduleRefId",
                table: "SemesterSchuduleSubject",
                column: "SemesterScheduleRefId");
        }
    }
}
