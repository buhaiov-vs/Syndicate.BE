using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syndicate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFolders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServicesFolders",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesFolders", x => x.Name);
                    table.ForeignKey(
                        name: "FK_ServicesFolders_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_FolderName",
                table: "Services",
                column: "FolderName");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesFolders_NormalizedName_OwnerId",
                table: "ServicesFolders",
                columns: new[] { "NormalizedName", "OwnerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesFolders_OwnerId",
                table: "ServicesFolders",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServicesFolders_FolderName",
                table: "Services",
                column: "FolderName",
                principalTable: "ServicesFolders",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServicesFolders_FolderName",
                table: "Services");

            migrationBuilder.DropTable(
                name: "ServicesFolders");

            migrationBuilder.DropIndex(
                name: "IX_Services_FolderName",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "Services");
        }
    }
}
