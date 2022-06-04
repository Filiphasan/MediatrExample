using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.Data.Migrations
{
    public partial class seeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "ID", "CREATED_TIME", "FIRST_NAME", "GSM", "LAST_NAME", "MAIL", "UPDATED_TIME" },
                values: new object[] { 1, new DateTime(2022, 6, 4, 22, 26, 12, 621, DateTimeKind.Local).AddTicks(8702), "Hasan", "5555555555", "Erdal", "test@test.com", new DateTime(2022, 6, 4, 22, 26, 12, 621, DateTimeKind.Local).AddTicks(8702) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
