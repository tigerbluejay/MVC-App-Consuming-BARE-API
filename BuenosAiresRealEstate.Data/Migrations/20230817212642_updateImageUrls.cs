using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuenosAiresRealEstate.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 18, 26, 42, 43, DateTimeKind.Local).AddTicks(7110), "\\Images\\Juncal.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 18, 26, 42, 43, DateTimeKind.Local).AddTicks(7123), "\\Images\\Guise.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 18, 26, 42, 43, DateTimeKind.Local).AddTicks(7125), "\\Images\\Beruti.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 18, 26, 42, 43, DateTimeKind.Local).AddTicks(7126), "\\Images\\Paraguay.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 18, 26, 42, 43, DateTimeKind.Local).AddTicks(7129), "\\Images\\Alcorta.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 18, 26, 42, 43, DateTimeKind.Local).AddTicks(7130), "\\Images\\SantaFe.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 18, 26, 42, 43, DateTimeKind.Local).AddTicks(7132), "\\Images\\Mansilla.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2359), "Images\\Juncal.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2373), "Images\\Guise.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2375), "Images\\Beruti.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2377), "Images\\Paraguay.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2380), "Images\\Alcorta.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2382), "Images\\SantaFe.jpg" });

            migrationBuilder.UpdateData(
                table: "ApartmentComplexes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 8, 17, 16, 21, 24, 28, DateTimeKind.Local).AddTicks(2384), "Images\\Mansilla.jpg" });
        }
    }
}
