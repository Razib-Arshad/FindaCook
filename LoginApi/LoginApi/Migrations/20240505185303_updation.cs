using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginApi.Migrations
{
    /// <inheritdoc />
    public partial class updation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "OrderRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "OrderRequest",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "OrderRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "OrderRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "OrderRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserContact",
                table: "OrderRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "selectedService",
                table: "OrderRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "OrderRequest");

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "OrderRequest");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderRequest");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "OrderRequest");

            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "OrderRequest");

            migrationBuilder.DropColumn(
                name: "UserContact",
                table: "OrderRequest");

            migrationBuilder.DropColumn(
                name: "selectedService",
                table: "OrderRequest");
        }
    }
}
