using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.ClassLib.Migrations
{
    public partial class commentsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Report_AdminConfirmed",
                table: "Comments",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Report_BlockApproved",
                table: "Comments",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Report_AdminConfirmed",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Report_BlockApproved",
                table: "Comments");
        }
    }
}
