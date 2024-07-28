using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Discount.gRPC.Migrations
{
    /// <inheritdoc />
    public partial class DiscountMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Description", "Percent", "ProductId" },
                values: new object[,]
                {
                    { 1, "IPhone X 256GB", 15, new Guid("6267e3aa-3e20-4600-8cf8-ae908a55eb30") },
                    { 2, "Laptop ASUS Travel Mate RAM 32GB", 20, new Guid("5853fe3d-677c-4bce-aa27-d12bf2b45e2e") },
                    { 3, "Samsung Galaxy 256GB", 15, new Guid("6267e3aa-3e20-4600-8cf8-ae908a55eb30") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
