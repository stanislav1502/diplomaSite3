using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaSite3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FacultyNumber = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Diploma",
                columns: table => new
                {
                    DiplomaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DefendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(5,3)", precision: 5, scale: 3, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diploma", x => x.DiplomaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diploma_StudentID",
                table: "Diploma",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Diploma_TeacherID",
                table: "Diploma",
                column: "TeacherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diploma");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
