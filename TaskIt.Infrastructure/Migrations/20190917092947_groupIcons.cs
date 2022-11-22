using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskItApi.Migrations
{
    public partial class groupIcons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Groups",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Groups",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Groups");
        }
    }
}
