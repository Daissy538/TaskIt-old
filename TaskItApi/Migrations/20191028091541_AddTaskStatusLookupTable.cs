﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskItApi.Migrations
{
    public partial class AddTaskStatusLookupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TaskStatus",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatus", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusID",
                table: "Tasks",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskStatus_StatusID",
                table: "Tasks",
                column: "StatusID",
                principalTable: "TaskStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskStatus_StatusID",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskStatus");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_StatusID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Tasks");
        }
    }
}
