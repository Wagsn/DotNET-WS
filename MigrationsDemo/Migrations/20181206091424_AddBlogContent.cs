using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationsDemo.Migrations
{
    public partial class AddBlogContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Blogs");
        }
    }
}
