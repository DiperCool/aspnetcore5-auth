using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Models.Migrations
{
    public partial class userEmailConfirm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "guidEmailConfirm",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "guidEmailConfirm",
                table: "Users");
        }
    }
}
