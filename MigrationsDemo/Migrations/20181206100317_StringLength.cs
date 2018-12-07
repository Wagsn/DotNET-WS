using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationsDemo.Migrations
{
    public partial class StringLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Blogs",
                maxLength: 511,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 511,
                oldNullable: true);
        }
    }
}
