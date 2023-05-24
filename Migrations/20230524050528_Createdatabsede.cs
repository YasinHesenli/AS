using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumia.Migrations
{
    public partial class Createdatabsede : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostionId",
                table: "teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostionId",
                table: "teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
