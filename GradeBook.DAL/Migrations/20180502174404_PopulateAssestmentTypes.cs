using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class PopulateAssestmentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("AssestmentType", "Description", "Екзамен");
            migrationBuilder.InsertData("AssestmentType", "Description", "Залік");
            migrationBuilder.InsertData("AssestmentType", "Description", "Диференційний залік");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE AssestmentType;");
        }
    }
}
