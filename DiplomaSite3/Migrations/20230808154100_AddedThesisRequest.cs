using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaSite3.Migrations
{
    /// <inheritdoc />
    public partial class AddedThesisRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestedThesesModelId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestedTheses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThesisID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedTheses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestedTheses_Thesis_ThesisID",
                        column: x => x.ThesisID,
                        principalTable: "Thesis",
                        principalColumn: "ThesisID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_RequestedThesesModelId",
                table: "Students",
                column: "RequestedThesesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedTheses_ThesisID",
                table: "RequestedTheses",
                column: "ThesisID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_RequestedTheses_RequestedThesesModelId",
                table: "Students",
                column: "RequestedThesesModelId",
                principalTable: "RequestedTheses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_RequestedTheses_RequestedThesesModelId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "RequestedTheses");

            migrationBuilder.DropIndex(
                name: "IX_Students_RequestedThesesModelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RequestedThesesModelId",
                table: "Students");
        }
    }
}
