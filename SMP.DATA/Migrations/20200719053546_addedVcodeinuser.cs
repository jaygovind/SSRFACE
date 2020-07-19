using Microsoft.EntityFrameworkCore.Migrations;

namespace SMP.DATA.Migrations
{
    public partial class addedVcodeinuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VCode",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VCode",
                table: "Users");
        }
    }
}
