using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediatrExample.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedTime",
                table: "USERS",
                newName: "UPDATED_TIME");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "USERS",
                newName: "CREATED_TIME");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UPDATED_TIME",
                table: "USERS",
                newName: "UpdatedTime");

            migrationBuilder.RenameColumn(
                name: "CREATED_TIME",
                table: "USERS",
                newName: "CreatedTime");
        }
    }
}
