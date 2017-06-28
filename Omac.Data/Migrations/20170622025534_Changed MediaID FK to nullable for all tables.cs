using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omack.Data.Migrations
{
    public partial class ChangedMediaIDFKtonullableforalltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Media_MediaId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Media_MediaId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Media_MediaId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MediaId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Item_MediaId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Group_MediaId",
                table: "Group");

            migrationBuilder.AlterColumn<int>(
                name: "MediaId",
                table: "User",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Notification",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Media",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MediaId",
                table: "Item",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MediaId",
                table: "Group",
                nullable: true,
                oldClrType: typeof(int));

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
                name: "FK_Group_Media_MediaId",
                table: "Group",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Media_MediaId",
                table: "Item",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Transaction_TransactionId",
                table: "Media",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Media_MediaId",
                table: "User",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Media_MediaId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Media_MediaId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Transaction_TransactionId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Media_MediaId",
                table: "User");

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

            migrationBuilder.AlterColumn<int>(
                name: "MediaId",
                table: "User",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Notification",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MediaId",
                table: "Item",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MediaId",
                table: "Group",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Media_MediaId",
                table: "Group",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Media_MediaId",
                table: "Item",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Media_MediaId",
                table: "User",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
