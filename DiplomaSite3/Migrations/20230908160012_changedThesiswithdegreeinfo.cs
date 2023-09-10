using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaSite3.Migrations
{
    /// <inheritdoc />
    public partial class changedThesiswithdegreeinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Degrees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Degrees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_DepartmentId",
                table: "Degrees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_FacultyId",
                table: "Degrees",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Degrees_Departments_DepartmentId",
                table: "Degrees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Degrees_Faculties_FacultyId",
                table: "Degrees",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Degrees_Departments_DepartmentId",
                table: "Degrees");

            migrationBuilder.DropForeignKey(
                name: "FK_Degrees_Faculties_FacultyId",
                table: "Degrees");

            migrationBuilder.DropIndex(
                name: "IX_Degrees_DepartmentId",
                table: "Degrees");

            migrationBuilder.DropIndex(
                name: "IX_Degrees_FacultyId",
                table: "Degrees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Degrees");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Degrees");
        }
    }
}
