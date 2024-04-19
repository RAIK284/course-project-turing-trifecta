using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SpectrumCardCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SpectrumCards",
                columns: new[] { "Id", "LeftName", "RightName" },
                values: new object[,]
                {
                    { new Guid("4508e7d3-2d87-4d3f-9811-a80ceef6c14b"), "Colorful", "Colorless" },
                    { new Guid("4776e959-b795-4ec1-aa5f-440d786387d3"), "Common", "Rare" },
                    { new Guid("77b7290e-583f-463d-b643-b961d353e7f3"), "Weird", "Normal" },
                    { new Guid("8e4a470c-1df2-4bf5-a034-2004628eae90"), "Highly Attractive", "Mildly Attractive" },
                    { new Guid("a32543d2-1da3-4d39-b533-66014de89889"), "Expensive", "Cheap" },
                    { new Guid("af711bff-924a-407b-b712-99e13b0cbf9f"), "Overrated Weapon", "Underrated Weapon" },
                    { new Guid("c5150589-e161-4096-9c73-e72b75e8d0e7"), "Cold", "Hot" },
                    { new Guid("ca7c9ab3-71b1-4376-a452-4e1ce108e070"), "High Calorie", "Low Calorie" },
                    { new Guid("d443399e-4292-45d3-903e-937743e049d3"), "Good", "Bad" },
                    { new Guid("d737e617-8b15-48aa-84b6-80eb11b8d09a"), "Feels Good", "Feels Bad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("4508e7d3-2d87-4d3f-9811-a80ceef6c14b"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("4776e959-b795-4ec1-aa5f-440d786387d3"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("77b7290e-583f-463d-b643-b961d353e7f3"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("8e4a470c-1df2-4bf5-a034-2004628eae90"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("a32543d2-1da3-4d39-b533-66014de89889"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("af711bff-924a-407b-b712-99e13b0cbf9f"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("c5150589-e161-4096-9c73-e72b75e8d0e7"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("ca7c9ab3-71b1-4376-a452-4e1ce108e070"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("d443399e-4292-45d3-903e-937743e049d3"));

            migrationBuilder.DeleteData(
                table: "SpectrumCards",
                keyColumn: "Id",
                keyValue: new Guid("d737e617-8b15-48aa-84b6-80eb11b8d09a"));
        }
    }
}
