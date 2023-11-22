using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheVoice.Data.Migrations
{
    public partial class add_Name_PropertyToModels_Mentor_Team_Candicate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Mentores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Candicates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Mentores");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Candicates");
        }
    }
}
