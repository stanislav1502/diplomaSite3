using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaSite3.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "AdminPass",
                table: "Admins");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Teachers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Admins");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Teachers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminPass",
                table: "Admins",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
