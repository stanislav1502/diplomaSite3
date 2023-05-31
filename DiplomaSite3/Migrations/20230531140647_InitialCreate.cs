
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
                name: "UserModel",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "DiplomaModel",
                columns: table => new
                {
                    DiplomaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(5,3)", precision: 5, scale: 3, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiplomaModel", x => x.DiplomaID);
                    table.ForeignKey(
                        name: "FK_DiplomaModel_UserModel_StudentID",
                        column: x => x.StudentID,
                        principalTable: "UserModel",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_DiplomaModel_UserModel_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "UserModel",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiplomaModel_StudentID",
                table: "DiplomaModel",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DiplomaModel_TeacherID",
                table: "DiplomaModel",
                column: "TeacherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiplomaModel");

            migrationBuilder.DropTable(
                name: "UserModel");
        }
    }
}
