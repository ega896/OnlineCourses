using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses.Persistence.Migrations
{
    public partial class FilePath_Added_To_Courses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                table: "Courses");
        }
    }
}
