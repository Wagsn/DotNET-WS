using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationsDemo.Migrations
{
    public partial class Range : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Blogs",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
