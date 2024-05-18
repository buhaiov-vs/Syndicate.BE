using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Syndicate.Data.Migrations
{
    /// <inheritdoc />
    public partial class A2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTag_Tags_TagsId",
                table: "ServiceTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTag",
                table: "ServiceTag");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTag_TagsId",
                table: "ServiceTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "ServiceTag");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "TagsName",
                table: "ServiceTag",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTag",
                table: "ServiceTag",
                columns: new[] { "ServicesId", "TagsName" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTag_TagsName",
                table: "ServiceTag",
                column: "TagsName");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTag_Tags_TagsName",
                table: "ServiceTag",
                column: "TagsName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTag_Tags_TagsName",
                table: "ServiceTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTag",
                table: "ServiceTag");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTag_TagsName",
                table: "ServiceTag");

            migrationBuilder.DropColumn(
                name: "TagsName",
                table: "ServiceTag");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TagsId",
                table: "ServiceTag",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTag",
                table: "ServiceTag",
                columns: new[] { "ServicesId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTag_TagsId",
                table: "ServiceTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTag_Tags_TagsId",
                table: "ServiceTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
