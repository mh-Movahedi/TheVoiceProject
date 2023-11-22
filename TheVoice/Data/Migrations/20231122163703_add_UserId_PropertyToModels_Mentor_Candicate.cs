using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheVoice.Data.Migrations
{
    public partial class add_UserId_PropertyToModels_Mentor_Candicate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Mentores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Candicates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Mentores");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Candicates");
        }
    }
}
