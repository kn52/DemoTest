using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPages.Migrations
{
    public partial class INITMIG3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SCode",
                table: "SDescriptions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PCode",
                table: "PDescriptions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SCode",
                table: "SDescriptions");

            migrationBuilder.DropColumn(
                name: "PCode",
                table: "PDescriptions");
        }
    }
}
