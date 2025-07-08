using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("c3d106b2-bf23-44d2-a8d4-44cfe7173e58"));

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "Description", "DurationInMonths", "Name", "PriceAmount", "PriceCurrency" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Абонемент на 1 месяц, чтобы познакомиться с клубом", 1, "Новичок", 3590m, "RUB" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "Description", "DurationInMonths", "Name", "PriceAmount", "PriceCurrency" },
                values: new object[] { new Guid("c3d106b2-bf23-44d2-a8d4-44cfe7173e58"), "Абонемент на 1 месяц, чтобы познакомиться с клубом", 1, "Новичок", 3590m, "RUB" });
        }
    }
}
