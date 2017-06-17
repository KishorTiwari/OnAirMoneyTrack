using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omack.Data.Migrations
{
    public partial class addedurlandtypeidtomediaandtransactionrespectively : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transaction",
                newName: "TypeId");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Media",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Media");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Transaction",
                newName: "Type");
        }
    }
}
