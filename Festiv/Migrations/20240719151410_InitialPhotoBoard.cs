using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Festiv.Migrations
{
    /// <inheritdoc />
    public partial class InitialPhotoBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eb4a059e-5d16-4494-9c25-d47be6758540"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("596cf31c-770a-4fee-97d2-3aeac9ae8bd2"), new Guid("698d8490-1398-4a3f-b321-993a2c7fd8e0") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("596cf31c-770a-4fee-97d2-3aeac9ae8bd2"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("698d8490-1398-4a3f-b321-993a2c7fd8e0"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7c0b8ea3-5dd7-4dfb-9445-df7602cf036d"), null, "User", "USER" },
                    { new Guid("b8045426-4c9a-41f6-913e-4a9071904257"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("297d9574-ec87-4995-8e35-e957a64a540d"), 0, "b0838cad-5b0a-4e94-a3fd-37bfa7ec6478", "admin@festiv.com", true, "Admin", "User", false, null, "ADMIN@FESTIV.COM", "ADMIN@FESTIV.COM", "AQAAAAIAAYagAAAAEN0LA1iyYFdqPlhMk9GuOUvV0nBZjC14yVVZFs883nykwlKASwjoNTaJpoX8xl2SQA==", null, false, null, 0, "", false, "admin@festiv.com", true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("b8045426-4c9a-41f6-913e-4a9071904257"), new Guid("297d9574-ec87-4995-8e35-e957a64a540d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7c0b8ea3-5dd7-4dfb-9445-df7602cf036d"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b8045426-4c9a-41f6-913e-4a9071904257"), new Guid("297d9574-ec87-4995-8e35-e957a64a540d") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b8045426-4c9a-41f6-913e-4a9071904257"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("297d9574-ec87-4995-8e35-e957a64a540d"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("596cf31c-770a-4fee-97d2-3aeac9ae8bd2"), null, "Admin", "ADMIN" },
                    { new Guid("eb4a059e-5d16-4494-9c25-d47be6758540"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("698d8490-1398-4a3f-b321-993a2c7fd8e0"), 0, "cdf1c47e-e15e-4701-b7a1-56b342328ff3", "admin@festiv.com", true, "Admin", "User", false, null, "ADMIN@FESTIV.COM", "ADMIN@FESTIV.COM", "AQAAAAIAAYagAAAAENEMnypZ2YBVRmWf0s3hGGYp/B+3lomGqkQRH0rtTmS1JVfui9RTMN5/aFRmdBdIlg==", null, false, null, 0, "", false, "admin@festiv.com", true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("596cf31c-770a-4fee-97d2-3aeac9ae8bd2"), new Guid("698d8490-1398-4a3f-b321-993a2c7fd8e0") });
        }
    }
}
