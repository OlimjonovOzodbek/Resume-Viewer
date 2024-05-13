using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addseeddata2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("12345678-1234-1234-1234-1234567890ab"),
                column: "Password",
                value: "8e3df55a92106cbffa53d9bf2f3e4efe656a1e4c00be7dccd0ee3386064e3601");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("12345678-1234-1234-1234-1234567890ab"),
                column: "Password",
                value: "SuperAdmin1");
        }
    }
}
