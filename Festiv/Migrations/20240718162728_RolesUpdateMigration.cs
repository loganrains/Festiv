using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Festiv.Migrations
{
    /// <inheritdoc />
    public partial class RolesUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("46f9e298-b395-4299-8366-a1f87f554111"), null, "User", "USER" },
                    { new Guid("d1d524f6-8297-4dd4-bff2-cf744c7e7b92"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("13792d3e-72b2-412e-baf1-5bfb514e1bfe"), 0, "b25a3a31-60f7-4768-8d9b-f1e266511369", "admin@festiv.com", true, "Admin", "User", false, null, "ADMIN@FESTIV.COM", "ADMIN@FESTIV.COM", "AQAAAAIAAYagAAAAEJ6+qzX9WySB2lOeMFOWgZ5Or94IBcCfmfXj2OBqi2ZVak9LGWOPcsHE/V2LKL5Xyg==", null, false, null, 0, "", false, "admin@festiv.com", true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d1d524f6-8297-4dd4-bff2-cf744c7e7b92"), new Guid("13792d3e-72b2-412e-baf1-5bfb514e1bfe") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("46f9e298-b395-4299-8366-a1f87f554111"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d1d524f6-8297-4dd4-bff2-cf744c7e7b92"), new Guid("13792d3e-72b2-412e-baf1-5bfb514e1bfe") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d1d524f6-8297-4dd4-bff2-cf744c7e7b92"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("13792d3e-72b2-412e-baf1-5bfb514e1bfe"));

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
    }
}
