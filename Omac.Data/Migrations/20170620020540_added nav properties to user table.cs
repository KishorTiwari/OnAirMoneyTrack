using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omack.Data.Migrations
{
    public partial class addednavpropertiestousertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Group",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_UserId",
                table: "Group",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_User_UserId",
                table: "Group",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_User_UserId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_UserId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Group");
        }
    }
}
