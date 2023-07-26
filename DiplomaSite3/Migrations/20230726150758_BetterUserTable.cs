using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaSite3.Migrations
{
    /// <inheritdoc />
    public partial class BetterUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Degrees_Departments_DepartmentId",
                table: "Degrees");

            migrationBuilder.DropForeignKey(
                name: "FK_Degrees_Faculties_FacultyId",
                table: "Degrees");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Programmes_Departments_DepartmentId",
                table: "Programmes");

            migrationBuilder.DropTable(
                name: "Diplomas");

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

            migrationBuilder.CreateTable(
                name: "Thesis",
                columns: table => new
                {
                    ThesisID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AssignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(5,3)", precision: 5, scale: 3, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DegreeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thesis", x => x.ThesisID);
                    table.ForeignKey(
                        name: "FK_Thesis_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedTheses",
                columns: table => new
                {
                    ThesisID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedTheses", x => x.ThesisID);
                    table.ForeignKey(
                        name: "FK_AssignedTheses_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignedTheses_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignedTheses_Thesis_ThesisID",
                        column: x => x.ThesisID,
                        principalTable: "Thesis",
                        principalColumn: "ThesisID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTheses_StudentID",
                table: "AssignedTheses",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTheses_TeacherID",
                table: "AssignedTheses",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_DegreeId",
                table: "Thesis",
                column: "DegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Programmes_Departments_DepartmentId",
                table: "Programmes",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Programmes_Departments_DepartmentId",
                table: "Programmes");

            migrationBuilder.DropTable(
                name: "AssignedTheses");

            migrationBuilder.DropTable(
                name: "Thesis");

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

            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    DiplomaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DegreeId = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DefendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Grade = table.Column<decimal>(type: "decimal(5,3)", precision: 5, scale: 3, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.DiplomaID);
                    table.ForeignKey(
                        name: "FK_Diplomas_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diplomas_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Diplomas_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_DepartmentId",
                table: "Degrees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_FacultyId",
                table: "Degrees",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_DegreeId",
                table: "Diplomas",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_StudentID",
                table: "Diplomas",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_TeacherID",
                table: "Diplomas",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Degrees_Departments_DepartmentId",
                table: "Degrees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Degrees_Faculties_FacultyId",
                table: "Degrees",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyId",
                table: "Departments",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Programmes_Departments_DepartmentId",
                table: "Programmes",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
