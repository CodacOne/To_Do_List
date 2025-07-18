using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "DueDate", "IsCompleted", "OwnerId", "Priority", "Title" },
                values: new object[,]
                {
                    { 1, "Development", new DateTime(2025, 7, 16, 20, 46, 44, 115, DateTimeKind.Utc).AddTicks(5489), "Create project structure and configure dependencies.", new DateTime(2025, 7, 19, 20, 46, 44, 115, DateTimeKind.Utc).AddTicks(5491), false, "user-1", 1, "Setup project" },
                    { 2, "Docs", new DateTime(2025, 7, 16, 20, 46, 44, 115, DateTimeKind.Utc).AddTicks(5501), "Document the API endpoints.", new DateTime(2025, 7, 21, 20, 46, 44, 115, DateTimeKind.Utc).AddTicks(5502), false, "user-2", 2, "Write documentation" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { "user-1", "admin@example.com", "admin-hash", "Owner", "admin" },
                    { "user-2", "guest@example.com", "guest-hash", "Guest", "guest" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "user-1");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "user-2");
        }
    }
}
