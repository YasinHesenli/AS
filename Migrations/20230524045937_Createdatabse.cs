using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumia.Migrations
{
    public partial class Createdatabse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostionID",
                table: "teams",
                newName: "PostionId");

            migrationBuilder.AlterColumn<int>(
                name: "PostionId",
                table: "teams",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostionId",
                table: "teams",
                newName: "PostionID");

            migrationBuilder.AlterColumn<string>(
                name: "PostionID",
                table: "teams",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
