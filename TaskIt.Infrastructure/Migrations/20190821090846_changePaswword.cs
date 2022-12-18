using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskItApi.Migrations
{
    public partial class changePaswword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users"
            );
            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Users",
                nullable: false,
                defaultValue: new byte[] { }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users"
            );

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                defaultValue: ""
            );
        }
    }
}
