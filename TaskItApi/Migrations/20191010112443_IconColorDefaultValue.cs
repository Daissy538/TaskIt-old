using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskItApi.Migrations
{
    public partial class IconColorDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "ColorID",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IconID",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ColorID",
                table: "Groups",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_IconID",
                table: "Groups",
                column: "IconID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Colors_ColorID",
                table: "Groups",
                column: "ColorID",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Icons_IconID",
                table: "Groups",
                column: "IconID",
                principalTable: "Icons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
            table: "Colors",
            columns: new[] { "ID", "Name", "Value" },
            values: new object[,]
            {
                            { 1, "Pink", "#ec407a" },
                            { 2, "Orange", "#ef5350" },
                            { 3, "Purple", "#ab47bc" },
                            { 4, "Blue", "#5c6bc0" }
            });

            migrationBuilder.InsertData(
                table: "Icons",
                columns: new[] { "ID", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "House", "house" },
                    { 2, "Work", "work" },
                    { 3, "Sport", "directions_run" },
                    { 4, "Education", "school" },
                    { 5, "Game", "headset_mic" },
                    { 6, "Music", "music_note" },
                    { 7, "Nature", "nature_people" },
                    { 8, "Voluntary work", "loyalty" },
                    { 9, "Animal", "pets" },
                    { 10, "Art", "color_lens" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Colors_ColorID",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Icons_IconID",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ColorID",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_IconID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ColorID",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IconID",
                table: "Groups");

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
    }
}
