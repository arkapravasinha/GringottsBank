using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GringottBank.DataAccess.EF.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "AuditLogs",
                newName: "TimeStamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 1, 0, 19, 9, 638, DateTimeKind.Local).AddTicks(2248),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 29, 22, 31, 29, 151, DateTimeKind.Local).AddTicks(1393));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "AuditLogs",
                newName: "DateTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 29, 22, 31, 29, 151, DateTimeKind.Local).AddTicks(1393),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 1, 0, 19, 9, 638, DateTimeKind.Local).AddTicks(2248));
        }
    }
}
