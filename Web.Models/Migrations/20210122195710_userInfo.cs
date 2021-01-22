using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Models.Migrations
{
    public partial class userInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailIsVerified",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailIsVerified",
                table: "Users");
        }
    }
}
