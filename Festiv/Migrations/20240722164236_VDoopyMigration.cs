using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Festiv.Migrations
{
    /// <inheritdoc />
    public partial class VDoopyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d5027e6a-ec8b-4fcd-9f8c-ff0d7540b098"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1664a2c2-930b-4fa0-a012-5430fbe667a9"), new Guid("ab08cb1a-d6f9-4afe-9633-f47371ac8383") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1664a2c2-930b-4fa0-a012-5430fbe667a9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ab08cb1a-d6f9-4afe-9633-f47371ac8383"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("ac24df23-20d0-4dfc-99ea-4d26322040c3"), null, "User", "USER" },
                    { new Guid("c731ed30-620d-45e3-92c3-613db7c55c2c"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("d4fba812-68d2-48de-b019-c1e2c57aa5b4"), 0, "bde752a3-9bdd-423b-8503-08eeeedac71f", "admin@festiv.com", true, "Admin", "User", false, null, "ADMIN@FESTIV.COM", "ADMIN@FESTIV.COM", "AQAAAAIAAYagAAAAEPLkY/e3f1nIiVQzFNyV9LI3G7rk0rAtHRGAARQvWuMhQwectkXMdkpkRG9sRmawGQ==", null, false, null, 0, "", false, "admin@festiv.com", true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("c731ed30-620d-45e3-92c3-613db7c55c2c"), new Guid("d4fba812-68d2-48de-b019-c1e2c57aa5b4") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ac24df23-20d0-4dfc-99ea-4d26322040c3"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c731ed30-620d-45e3-92c3-613db7c55c2c"), new Guid("d4fba812-68d2-48de-b019-c1e2c57aa5b4") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c731ed30-620d-45e3-92c3-613db7c55c2c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d4fba812-68d2-48de-b019-c1e2c57aa5b4"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1664a2c2-930b-4fa0-a012-5430fbe667a9"), null, "Admin", "ADMIN" },
                    { new Guid("d5027e6a-ec8b-4fcd-9f8c-ff0d7540b098"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("ab08cb1a-d6f9-4afe-9633-f47371ac8383"), 0, "48399bc3-f0ae-4ac7-ba62-e71df8df6cca", "admin@festiv.com", true, "Admin", "User", false, null, "ADMIN@FESTIV.COM", "ADMIN@FESTIV.COM", "AQAAAAIAAYagAAAAEO5c3QXpwaIuN9zjGSvdTJjuZLhDXHGHxQz7FP/jMXWf+ucYzjtGJBUHvR4ZhntCug==", null, false, null, 0, "", false, "admin@festiv.com", true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1664a2c2-930b-4fa0-a012-5430fbe667a9"), new Guid("ab08cb1a-d6f9-4afe-9633-f47371ac8383") });
        }
    }
}
