using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaSite3.Migrations
{
    /// <inheritdoc />
    public partial class RedoingRequestedTheses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thesis_RequestedTheses_RequestedThesesModelRequestId",
                table: "Thesis");

            migrationBuilder.DropTable(
                name: "RequestedThesesModelStudentModel");

            migrationBuilder.DropIndex(
                name: "IX_Thesis_RequestedThesesModelRequestId",
                table: "Thesis");

            migrationBuilder.DropColumn(
                name: "RequestedThesesModelRequestId",
                table: "Thesis");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedTheses_StudentID",
                table: "RequestedTheses",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedTheses_ThesisID",
                table: "RequestedTheses",
                column: "ThesisID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedTheses_Students_StudentID",
                table: "RequestedTheses",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedTheses_Thesis_ThesisID",
                table: "RequestedTheses",
                column: "ThesisID",
                principalTable: "Thesis",
                principalColumn: "ThesisID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedTheses_Students_StudentID",
                table: "RequestedTheses");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestedTheses_Thesis_ThesisID",
                table: "RequestedTheses");

            migrationBuilder.DropIndex(
                name: "IX_RequestedTheses_StudentID",
                table: "RequestedTheses");

            migrationBuilder.DropIndex(
                name: "IX_RequestedTheses_ThesisID",
                table: "RequestedTheses");

            migrationBuilder.AddColumn<int>(
                name: "RequestedThesesModelRequestId",
                table: "Thesis",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestedThesesModelStudentModel",
                columns: table => new
                {
                    RequestedThesesRequestId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedThesesModelStudentModel", x => new { x.RequestedThesesRequestId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_RequestedThesesModelStudentModel_RequestedTheses_RequestedThesesRequestId",
                        column: x => x.RequestedThesesRequestId,
                        principalTable: "RequestedTheses",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestedThesesModelStudentModel_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_RequestedThesesModelRequestId",
                table: "Thesis",
                column: "RequestedThesesModelRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedThesesModelStudentModel_StudentsId",
                table: "RequestedThesesModelStudentModel",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thesis_RequestedTheses_RequestedThesesModelRequestId",
                table: "Thesis",
                column: "RequestedThesesModelRequestId",
                principalTable: "RequestedTheses",
                principalColumn: "RequestId");
        }
    }
}
