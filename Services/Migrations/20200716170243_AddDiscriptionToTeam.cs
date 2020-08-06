using Microsoft.EntityFrameworkCore.Migrations;

namespace BSA_EF.DAL.Migrations
{
    public partial class AddDiscriptionToTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discription",
                table: "Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discription",
                table: "Teams");
        }
    }
}
