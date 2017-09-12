using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omack.Data.OmackMigration
{
    public partial class delnavpropertiesfrommedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Transaction_TransactionId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_User_MediaId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Media_TransactionId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Item_MediaId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Group_MediaId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Media");

            migrationBuilder.CreateIndex(
                name: "IX_User_MediaId",
                table: "User",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_MediaId",
                table: "Item",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_MediaId",
                table: "Group",
                column: "MediaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_MediaId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Item_MediaId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Group_MediaId",
                table: "Group");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Media",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_MediaId",
                table: "User",
                column: "MediaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_TransactionId",
                table: "Media",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_MediaId",
                table: "Item",
                column: "MediaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_MediaId",
                table: "Group",
                column: "MediaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Transaction_TransactionId",
                table: "Media",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
