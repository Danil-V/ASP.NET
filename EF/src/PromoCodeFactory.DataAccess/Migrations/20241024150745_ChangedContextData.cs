using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedContextData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPreferences_Customers_CustomerId",
                table: "CustomerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPreferences_Preferences_PreferenceId",
                table: "CustomerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84") });

            migrationBuilder.DeleteData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd") });

            migrationBuilder.DeleteData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c") });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"));

            migrationBuilder.DeleteData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("53729346-a368-4eeb-8bfa-cc69b6050d21"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"));

            migrationBuilder.DeleteData(
                table: "Preferences",
                keyColumn: "Id",
                keyValue: new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"));

            migrationBuilder.DeleteData(
                table: "Preferences",
                keyColumn: "Id",
                keyValue: new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"));

            migrationBuilder.DeleteData(
                table: "Preferences",
                keyColumn: "Id",
                keyValue: new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPreferences_Customers_CustomerId",
                table: "CustomerPreferences",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPreferences_Preferences_PreferenceId",
                table: "CustomerPreferences",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPreferences_Customers_CustomerId",
                table: "CustomerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPreferences_Preferences_PreferenceId",
                table: "CustomerPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

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
                    { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"), new Guid("e5750358-0e58-4580-8abb-bec42c28d5b1"), null },
                    { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"), new Guid("6587e0dc-e2ae-4feb-a168-00f10e04a97a"), null },
                    { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), new Guid("8c216b85-7548-4d63-8944-bab429572a12"), null }
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
                values: new object[] { new Guid("53729346-a368-4eeb-8bfa-cc69b6050d21"), new DateTime(2024, 10, 22, 19, 31, 30, 285, DateTimeKind.Local).AddTicks(7558), "EASY PEASY -20% OFF", new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), "Иван Петров", new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), "Скидка для сотрудников" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPreferences_Customers_CustomerId",
                table: "CustomerPreferences",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPreferences_Preferences_PreferenceId",
                table: "CustomerPreferences",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
