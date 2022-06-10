using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.Data.Migrations
{
    public partial class pwalani : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_TIME",
                table: "USERS",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATED_TIME",
                table: "USERS",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PW_HASH",
                table: "USERS",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CREATED_TIME", "PW_HASH", "UPDATED_TIME" },
                values: new object[] { new DateTime(2022, 6, 10, 18, 42, 15, 172, DateTimeKind.Local).AddTicks(478), "", new DateTime(2022, 6, 10, 18, 42, 15, 172, DateTimeKind.Local).AddTicks(480) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PW_HASH",
                table: "USERS");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UPDATED_TIME",
                table: "USERS",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATED_TIME",
                table: "USERS",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CREATED_TIME", "UPDATED_TIME" },
                values: new object[] { new DateTime(2022, 6, 4, 22, 26, 12, 621, DateTimeKind.Local).AddTicks(8702), new DateTime(2022, 6, 4, 22, 26, 12, 621, DateTimeKind.Local).AddTicks(8702) });
        }
    }
}
