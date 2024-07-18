using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Festiv.Migrations
{
    /// <inheritdoc />
    public partial class UserTypeAddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("376cbd35-d14c-4e89-b1c3-555833fe34ba"), null, "User", "USER" },
                    { new Guid("447de846-faed-4071-bf3d-32f32c97eab0"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("bcc18eac-8d11-4d50-9a1b-4a64519cfbb2"), 0, "48fdae92-164b-42d1-abe1-d0c973902742", "admin@festiv.com", true, "Admin", "User", false, null, "ADMIN@FESTIV.COM", "ADMIN@FESTIV.COM", "AQAAAAIAAYagAAAAEKfCo0QkcY7mPEBri9fUQQytevDnR8Pm3mokLdhR255UIm3PiIjSij2vuc63Ciljyw==", null, false, null, 0, "", false, "admin@festiv.com", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("376cbd35-d14c-4e89-b1c3-555833fe34ba"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("447de846-faed-4071-bf3d-32f32c97eab0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("bcc18eac-8d11-4d50-9a1b-4a64519cfbb2"));
        }
    }
}
