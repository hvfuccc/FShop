using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntityImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "Images",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3cd4aae4-8648-4cd3-9ca8-ffd93d7a316d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "47ac2e75-8389-4ddb-ad7a-e2feb189118b", "AQAAAAIAAYagAAAAEDxHE/9beB8/Wd/ypBPX+ZPU48NxU1Tmu9RcauuNjuoAk9JDClJdb9RsoBIVs4tkRA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "Images",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3cd4aae4-8648-4cd3-9ca8-ffd93d7a316d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5b87c123-6372-4fd5-a5a8-ff1d429140bf", "AQAAAAIAAYagAAAAEG4DNjjepaktHai4rvAI8dZxSsVc+CxMrkpIx/S3smQcQ3wDpSOiXxoSUWx8lwXjAA==" });
        }
    }
}
