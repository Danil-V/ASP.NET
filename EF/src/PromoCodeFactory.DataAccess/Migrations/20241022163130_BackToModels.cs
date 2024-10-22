using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BackToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "AppliedPromocodesCount",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84") },
                column: "Id",
                value: new Guid("e5750358-0e58-4580-8abb-bec42c28d5b1"));

            migrationBuilder.UpdateData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd") },
                column: "Id",
                value: new Guid("6587e0dc-e2ae-4feb-a168-00f10e04a97a"));

            migrationBuilder.UpdateData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c") },
                column: "Id",
                value: new Guid("8c216b85-7548-4d63-8944-bab429572a12"));

            migrationBuilder.UpdateData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("53729346-a368-4eeb-8bfa-cc69b6050d21"),
                column: "BeginDate",
                value: new DateTime(2024, 10, 22, 19, 31, 30, 285, DateTimeKind.Local).AddTicks(7558));

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes");

            migrationBuilder.AddColumn<int>(
                name: "AppliedPromocodesCount",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84") },
                column: "Id",
                value: new Guid("dc80c6f2-f4c1-4ede-b1be-34ca40abc140"));

            migrationBuilder.UpdateData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd") },
                column: "Id",
                value: new Guid("f84d36a9-c168-447f-9c61-1880e681ebc4"));

            migrationBuilder.UpdateData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c") },
                column: "Id",
                value: new Guid("4a5cf896-c8f3-4a87-bf77-03832dad25aa"));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"),
                column: "AppliedPromocodesCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("53729346-a368-4eeb-8bfa-cc69b6050d21"),
                column: "BeginDate",
                value: new DateTime(2024, 10, 22, 18, 46, 47, 473, DateTimeKind.Local).AddTicks(9791));

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
