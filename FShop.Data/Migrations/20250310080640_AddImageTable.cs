using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3cd4aae4-8648-4cd3-9ca8-ffd93d7a316d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e48baa9c-5902-4bc9-9689-58c19970f999", "AQAAAAIAAYagAAAAEO/gj5XgNKD9Jvoqaj5vLcWk0GSrVtejKfgtGKFFcAjpFRo1vr7fhwNbfWqwqlEyDg==" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3cd4aae4-8648-4cd3-9ca8-ffd93d7a316d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1acfc960-b236-4ab7-a67c-938aaa41ad7b", "AQAAAAIAAYagAAAAEP0UqkGQWBlR6RbRiMaBTYv79Y1nIG5pvMNjyiidU43uVBfIwq70EzE2Sasrf3wKXw==" });
        }
    }
}
