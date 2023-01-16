using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationCoreLogin.Migrations
{
    public partial class UserTableProfilResimDosyasıEkle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilResimDosyasi",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "user_1.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilResimDosyasi",
                table: "User");
        }
    }
}
