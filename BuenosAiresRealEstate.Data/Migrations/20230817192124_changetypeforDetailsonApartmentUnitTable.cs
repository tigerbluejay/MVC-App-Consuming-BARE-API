using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuenosAiresRealEstate.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class changetypeforDetailsonApartmentUnitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "ApartmentUnits",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2359));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2373));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2375));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2377));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2380));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2382));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2384));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Details",
                table: "ApartmentUnits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 7, 14, 3, 12, 570, DateTimeKind.Local).AddTicks(6683));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 7, 14, 3, 12, 570, DateTimeKind.Local).AddTicks(6695));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 7, 14, 3, 12, 570, DateTimeKind.Local).AddTicks(6698));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 7, 14, 3, 12, 570, DateTimeKind.Local).AddTicks(6699));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 7, 14, 3, 12, 570, DateTimeKind.Local).AddTicks(6701));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 7, 14, 3, 12, 570, DateTimeKind.Local).AddTicks(6703));

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 7, 14, 3, 12, 570, DateTimeKind.Local).AddTicks(6705));
        }
    }
}
