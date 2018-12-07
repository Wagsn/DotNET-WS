using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationsDemo.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blogs",
                maxLength: 31,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blogs",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 31,
                oldNullable: true);
        }
    }
}
