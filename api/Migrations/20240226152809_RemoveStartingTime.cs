using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStartingTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36dc75ac-5c85-4d15-8365-e3d54a6d0f0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7f5069d-2908-46e5-901f-0f92edbbe7cd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43f805d5-f3f9-49d2-a2c9-eacd7b83145a", null, "Admin", "ADMIN" },
                    { "dc5dd744-0948-44fb-af81-52c35a2986ac", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43f805d5-f3f9-49d2-a2c9-eacd7b83145a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc5dd744-0948-44fb-af81-52c35a2986ac");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36dc75ac-5c85-4d15-8365-e3d54a6d0f0d", null, "Admin", "ADMIN" },
                    { "e7f5069d-2908-46e5-901f-0f92edbbe7cd", null, "User", "USER" }
                });
        }
    }
}
