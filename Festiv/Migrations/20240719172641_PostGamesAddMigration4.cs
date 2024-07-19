using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Festiv.Migrations
{
    /// <inheritdoc />
    public partial class PostGamesAddMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("64419f99-aa47-4d91-b95c-b74913dd42f5"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1324a01f-b410-45a6-84b3-27bfa7512353"), new Guid("985f2faf-b07f-4cc8-bd73-41c56700e74f") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1324a01f-b410-45a6-84b3-27bfa7512353"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("985f2faf-b07f-4cc8-bd73-41c56700e74f"));

            migrationBuilder.AddColumn<int>(
                name: "GiftId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gifts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("259de78d-913e-4b38-987a-36628708c3cd"), null, "User", "USER" },
                    { new Guid("bf09a51c-5608-4cb4-897f-60088b30ce3b"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "GiftId", "GuestRespondId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("04c9496f-b68a-460a-bed6-3927220980d9"), 0, "6089d3e6-2d9c-4124-8585-34cd34fa0d0c", "admin@festiv.com", true, "Admin", 0, 0, "User", false, null, "ADMIN@FESTIV.COM", "ADMIN@FESTIV.COM", "AQAAAAIAAYagAAAAEAJpTNnI6lhoKbCMcJPWptppgqkSpRm09K8MQnvXwLoXz/FdgMVL0rRvyxCz7aaz1Q==", null, false, null, 0, "", false, "admin@festiv.com", true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("bf09a51c-5608-4cb4-897f-60088b30ce3b"), new Guid("04c9496f-b68a-460a-bed6-3927220980d9") });

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_UserId",
                table: "Gifts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("259de78d-913e-4b38-987a-36628708c3cd"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("bf09a51c-5608-4cb4-897f-60088b30ce3b"), new Guid("04c9496f-b68a-460a-bed6-3927220980d9") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bf09a51c-5608-4cb4-897f-60088b30ce3b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("04c9496f-b68a-460a-bed6-3927220980d9"));

            migrationBuilder.DropColumn(
                name: "GiftId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1324a01f-b410-45a6-84b3-27bfa7512353"), null, "Admin", "ADMIN" },
                    { new Guid("64419f99-aa47-4d91-b95c-b74913dd42f5"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "GuestRespondId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePic", "Rating", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { new Guid("985f2faf-b07f-4cc8-bd73-41c56700e74f"), 0, "ffe76ace-c188-446f-888d-a9433e37bc83", "admin@festiv.com", true, "Admin", 0, "User", false, null, "ADMIN@FESTIV.COM", "ADMIN@FESTIV.COM", "AQAAAAIAAYagAAAAEL3lnMvi7xlO5HIltDaMzNcVHvWnxUWknXxmP8sfcc00RgKhM6dzZu17GH/g0nmuUg==", null, false, null, 0, "", false, "admin@festiv.com", true });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1324a01f-b410-45a6-84b3-27bfa7512353"), new Guid("985f2faf-b07f-4cc8-bd73-41c56700e74f") });
        }
    }
}
