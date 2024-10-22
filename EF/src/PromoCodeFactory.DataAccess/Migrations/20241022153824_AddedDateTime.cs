using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preferences_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    AppliedPromocodesCount = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    ServiceInfo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    BeginDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PartnerName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreferenceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromoCodes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoCodes_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPreferences",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreferenceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PromoCodeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPreferences", x => new { x.CustomerId, x.PreferenceId });
                    table.ForeignKey(
                        name: "FK_CustomerPreferences_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPreferences_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPreferences_PromoCodes_PromoCodeId",
                        column: x => x.PromoCodeId,
                        principalTable: "PromoCodes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "FullName", "LastName" },
                values: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), "ivan_sergeev@mail.ru", "Иван", null, "Петров" });

            migrationBuilder.InsertData(
                table: "Preferences",
                columns: new[] { "Id", "CustomerId", "Name" },
                values: new object[,]
                {
                    { new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"), null, "Дети" },
                    { new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"), null, "Семья" },
                    { new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), null, "Театр" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"), "Администратор", "Admin" },
                    { new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"), "Партнерский менеджер", "PartnerManager" }
                });

            migrationBuilder.InsertData(
                table: "CustomerPreferences",
                columns: new[] { "CustomerId", "PreferenceId", "Id", "PromoCodeId" },
                values: new object[,]
                {
                    { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"), new Guid("13efd743-71ac-43bb-9ff3-58a0a5a1feab"), null },
                    { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"), new Guid("70ca6ca7-4ab6-4a83-8fac-2995da48f801"), null },
                    { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), new Guid("bbdb7b6a-44f8-4abd-9833-cb8dd249fffc"), null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AppliedPromocodesCount", "Email", "FirstName", "LastName", "RoleId" },
                values: new object[,]
                {
                    { new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"), 5, "owner@somemail.ru", "Иван", "Сергеев", new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02") },
                    { new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"), 10, "andreev@somemail.ru", "Петр", "Андреев", new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665") }
                });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "BeginDate", "Code", "CustomerId", "EndDate", "PartnerName", "PreferenceId", "ServiceInfo" },
                values: new object[] { new Guid("53729346-a368-4eeb-8bfa-cc69b6050d21"), new DateTime(2024, 10, 22, 18, 38, 23, 858, DateTimeKind.Local).AddTicks(8378), "EASY PEASY -20% OFF", new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), "Иван Петров", new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), "Скидка для сотрудников" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPreferences_PreferenceId",
                table: "CustomerPreferences",
                column: "PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPreferences_PromoCodeId",
                table: "CustomerPreferences",
                column: "PromoCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_CustomerId",
                table: "Preferences",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_CustomerId",
                table: "PromoCodes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerPreferences");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PromoCodes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
