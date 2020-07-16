using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMP.DATA.Migrations
{
    public partial class addednewtablecommensubv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    ComID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentMsg = table.Column<string>(nullable: true),
                    CommentedDate = table.Column<DateTime>(nullable: true),
                    PostID = table.Column<int>(nullable: true),
                    CommentedUserID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.ComID);
                });

            migrationBuilder.CreateTable(
                name: "SubComment",
                columns: table => new
                {
                    SubComID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentMsg = table.Column<string>(nullable: true),
                    CommentedDate = table.Column<DateTime>(nullable: true),
                    ComentID = table.Column<int>(nullable: true),
                    SubComUserID = table.Column<int>(nullable: true),
                    Isdeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubComment", x => x.SubComID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "SubComment");
        }
    }
}
