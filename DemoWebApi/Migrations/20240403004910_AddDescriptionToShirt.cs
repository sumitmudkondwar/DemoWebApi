﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToShirt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shirt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Shirt",
                keyColumn: "ShirtId",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Shirt",
                keyColumn: "ShirtId",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Shirt",
                keyColumn: "ShirtId",
                keyValue: 3,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Shirt",
                keyColumn: "ShirtId",
                keyValue: 4,
                column: "Description",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shirt");
        }
    }
}
