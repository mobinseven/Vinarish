using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinarishMvc.Migrations
{
    public partial class DeviceTypeForStatusNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStatus_DeviceTypes_DeviceTypeId",
                table: "DeviceStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "DeviceStatus",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceTypeId",
                table: "DeviceStatus",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStatus_Text",
                table: "DeviceStatus",
                column: "Text",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStatus_DeviceTypes_DeviceTypeId",
                table: "DeviceStatus",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "DeviceTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStatus_DeviceTypes_DeviceTypeId",
                table: "DeviceStatus");

            migrationBuilder.DropIndex(
                name: "IX_DeviceStatus_Text",
                table: "DeviceStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "DeviceStatus",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "DeviceTypeId",
                table: "DeviceStatus",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStatus_DeviceTypes_DeviceTypeId",
                table: "DeviceStatus",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "DeviceTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
