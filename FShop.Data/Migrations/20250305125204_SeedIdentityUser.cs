using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("e99c6b6d-9056-41bd-8a48-971205c57824"), null, "Admin Role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e99c6b6d-9056-41bd-8a48-971205c57824"), new Guid("3cd4aae4-8648-4cd3-9ca8-ffd93d7a316d") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("3cd4aae4-8648-4cd3-9ca8-ffd93d7a316d"), 0, "1acfc960-b236-4ab7-a67c-938aaa41ad7b", new DateTime(2002, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "lmht721@gmail.com", true, "Phúc", "Hồ Văn", false, null, "lmht721@gmail.com", "admin", "AQAAAAIAAYagAAAAEP0UqkGQWBlR6RbRiMaBTYv79Y1nIG5pvMNjyiidU43uVBfIwq70EzE2Sasrf3wKXw==", null, false, "", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e99c6b6d-9056-41bd-8a48-971205c57824"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e99c6b6d-9056-41bd-8a48-971205c57824"), new Guid("3cd4aae4-8648-4cd3-9ca8-ffd93d7a316d") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3cd4aae4-8648-4cd3-9ca8-ffd93d7a316d"));
        }
    }
}
