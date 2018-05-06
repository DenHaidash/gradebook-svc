using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class PopulateAssetmentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("AssestmentTypes", "Description", "Екзамен");
            migrationBuilder.InsertData("AssestmentTypes", "Description", "Залік");
            migrationBuilder.InsertData("AssestmentTypes", "Description", "Диференційний залік");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE AssestmentTypes;");
        }
    }
}
