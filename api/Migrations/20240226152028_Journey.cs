using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Journey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "027a36fd-5123-4e0a-aee9-844318277dab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a31ebeb-bc82-4434-bd43-d1a5c9429918");

            migrationBuilder.DropColumn(
                name: "StartingTime",
                table: "Journeys");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36dc75ac-5c85-4d15-8365-e3d54a6d0f0d", null, "Admin", "ADMIN" },
                    { "e7f5069d-2908-46e5-901f-0f92edbbe7cd", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36dc75ac-5c85-4d15-8365-e3d54a6d0f0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7f5069d-2908-46e5-901f-0f92edbbe7cd");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingTime",
                table: "Journeys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "027a36fd-5123-4e0a-aee9-844318277dab", null, "User", "USER" },
                    { "2a31ebeb-bc82-4434-bd43-d1a5c9429918", null, "Admin", "ADMIN" }
                });
        }
    }
}
