using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Omack.Data.OmackLogMigration
{
    public partial class initialomacklog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Application = table.Column<string>(maxLength: 50, nullable: true),
                    CallSite = table.Column<string>(maxLength: 500, nullable: true),
                    Exception = table.Column<string>(maxLength: 500, nullable: true),
                    Level = table.Column<string>(maxLength: 50, nullable: true),
                    Logged = table.Column<DateTime>(nullable: false),
                    Logger = table.Column<string>(maxLength: 500, nullable: true),
                    MachineName = table.Column<string>(maxLength: 50, nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: true),
                    ThreadId = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Application = table.Column<string>(maxLength: 50, nullable: true),
                    CallSite = table.Column<string>(maxLength: 500, nullable: true),
                    Exception = table.Column<string>(maxLength: 500, nullable: true),
                    Level = table.Column<string>(maxLength: 50, nullable: true),
                    Logged = table.Column<DateTime>(nullable: false),
                    Logger = table.Column<string>(maxLength: 500, nullable: true),
                    MachineName = table.Column<string>(maxLength: 50, nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: true),
                    ThreadId = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLog");

            migrationBuilder.DropTable(
                name: "WebLog");
        }
    }
}
