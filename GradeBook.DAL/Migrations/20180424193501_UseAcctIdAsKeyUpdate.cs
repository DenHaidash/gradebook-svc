using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GradeBook.DAL.Migrations
{
    public partial class UseAcctIdAsKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Accounts_AccountRefId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Accounts_AccountRefId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "AccountRefId",
                table: "Teachers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AccountRefId",
                table: "Students",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Accounts_Id",
                table: "Students",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Accounts_Id",
                table: "Teachers",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Accounts_Id",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Accounts_Id",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Teachers",
                newName: "AccountRefId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "AccountRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Accounts_AccountRefId",
                table: "Students",
                column: "AccountRefId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Accounts_AccountRefId",
                table: "Teachers",
                column: "AccountRefId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
