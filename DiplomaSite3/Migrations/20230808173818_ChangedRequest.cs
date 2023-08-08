using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaSite3.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedTheses_Thesis_ThesisID",
                table: "RequestedTheses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_RequestedTheses_RequestedThesesModelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_RequestedThesesModelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_RequestedTheses_ThesisID",
                table: "RequestedTheses");

            migrationBuilder.DropColumn(
                name: "RequestedThesesModelId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "RequestedThesisModelId",
                table: "Thesis",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestedThesisModelStudentModel",
                columns: table => new
                {
                    RequestedThesesId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedThesisModelStudentModel", x => new { x.RequestedThesesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_RequestedThesisModelStudentModel_RequestedTheses_RequestedThesesId",
                        column: x => x.RequestedThesesId,
                        principalTable: "RequestedTheses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestedThesisModelStudentModel_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_RequestedThesisModelId",
                table: "Thesis",
                column: "RequestedThesisModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedThesisModelStudentModel_StudentsId",
                table: "RequestedThesisModelStudentModel",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thesis_RequestedTheses_RequestedThesisModelId",
                table: "Thesis",
                column: "RequestedThesisModelId",
                principalTable: "RequestedTheses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thesis_RequestedTheses_RequestedThesisModelId",
                table: "Thesis");

            migrationBuilder.DropTable(
                name: "RequestedThesisModelStudentModel");

            migrationBuilder.DropIndex(
                name: "IX_Thesis_RequestedThesisModelId",
                table: "Thesis");

            migrationBuilder.DropColumn(
                name: "RequestedThesisModelId",
                table: "Thesis");

            migrationBuilder.AddColumn<int>(
                name: "RequestedThesesModelId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_RequestedThesesModelId",
                table: "Students",
                column: "RequestedThesesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedTheses_ThesisID",
                table: "RequestedTheses",
                column: "ThesisID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedTheses_Thesis_ThesisID",
                table: "RequestedTheses",
                column: "ThesisID",
                principalTable: "Thesis",
                principalColumn: "ThesisID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_RequestedTheses_RequestedThesesModelId",
                table: "Students",
                column: "RequestedThesesModelId",
                principalTable: "RequestedTheses",
                principalColumn: "Id");
        }
    }
}
