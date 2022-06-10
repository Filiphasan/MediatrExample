using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.Data.Migrations
{
    public partial class pwadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CREATED_TIME", "PW_HASH", "UPDATED_TIME" },
                values: new object[] { new DateTime(2022, 6, 10, 18, 45, 31, 690, DateTimeKind.Local).AddTicks(3781), "E10ADC3949BA59ABBE56E057F20F883E", new DateTime(2022, 6, 10, 18, 45, 31, 690, DateTimeKind.Local).AddTicks(3783) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CREATED_TIME", "PW_HASH", "UPDATED_TIME" },
                values: new object[] { new DateTime(2022, 6, 10, 18, 42, 15, 172, DateTimeKind.Local).AddTicks(478), "", new DateTime(2022, 6, 10, 18, 42, 15, 172, DateTimeKind.Local).AddTicks(480) });
        }
    }
}
