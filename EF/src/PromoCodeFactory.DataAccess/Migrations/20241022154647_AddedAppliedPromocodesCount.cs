using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppliedPromocodesCount : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                value: new Guid("13efd743-71ac-43bb-9ff3-58a0a5a1feab"));

            migrationBuilder.UpdateData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd") },
                column: "Id",
                value: new Guid("70ca6ca7-4ab6-4a83-8fac-2995da48f801"));

            migrationBuilder.UpdateData(
                table: "CustomerPreferences",
                keyColumns: new[] { "CustomerId", "PreferenceId" },
                keyValues: new object[] { new Guid("451423d5-d8d5-4a11-9c7b-eb9f14e1a72f"), new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c") },
                column: "Id",
                value: new Guid("bbdb7b6a-44f8-4abd-9833-cb8dd249fffc"));

            migrationBuilder.UpdateData(
                table: "PromoCodes",
                keyColumn: "Id",
                keyValue: new Guid("53729346-a368-4eeb-8bfa-cc69b6050d21"),
                column: "BeginDate",
                value: new DateTime(2024, 10, 22, 18, 38, 23, 858, DateTimeKind.Local).AddTicks(8378));

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

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCodes_Preferences_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
