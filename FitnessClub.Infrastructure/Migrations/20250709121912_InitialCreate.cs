using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessClub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PriceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    DurationInMonths = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Plans_MembershipPlanId",
                        column: x => x.MembershipPlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "Description", "DurationInMonths", "Name", "PriceAmount", "PriceCurrency" },
                values: new object[,]
                {
                    { new Guid("3c1121e5-7282-4d1f-9d57-680b9e36462b"), "Стандартный абонемент на 1 месяц.", 1, "Стандарт 1", 3990m, "RUB" },
                    { new Guid("4bfb3e5e-9d54-40cd-9388-4ad78acdaef4"), "Выгодный абонемент на 3 месяца.", 3, "Стандарт 3", 10990m, "RUB" },
                    { new Guid("78dcd9cd-2373-4cf0-8f64-f32464961788"), "Абонемент для посещений в дневные часы (c 12:00 до 17:00).", 1, "Дневной", 3290m, "RUB" },
                    { new Guid("9ba1a5e7-35cc-4e91-93be-1fe2e27de326"), "Годовой абонемент с максимальной выгодой.", 12, "Стандарт 12", 35990m, "RUB" },
                    { new Guid("ac75452c-6ab6-42cc-9d24-b5e120984286"), "Полугодовой абонемент для регулярных занятий.", 6, "Стандарт 6", 19990m, "RUB" },
                    { new Guid("b5adb578-bc5d-4d48-a7f1-137185eb9661"), "Абонемент для посещений в утренние часы (c 6:00 до 12:00).", 1, "Утренний", 2990m, "RUB" },
                    { new Guid("d86dec31-d099-43f0-b860-8976f44e50ff"), "Специальный абонемент для студентов на 3 месяца.", 3, "Студенческий", 8990m, "RUB" },
                    { new Guid("e356bb37-2be5-4ad6-9a66-339e4f10094a"), "Абонемент на 1 месяц, чтобы познакомиться с клубом.", 1, "Новичок", 3590m, "RUB" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_MembershipPlanId",
                table: "Subscriptions",
                column: "MembershipPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
