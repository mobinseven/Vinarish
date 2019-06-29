using Microsoft.EntityFrameworkCore.Migrations;

namespace VinarishMvc.Migrations
{
    public partial class StatusTextIsNotUnique2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeviceStatus_Text",
                table: "DeviceStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "DeviceStatus",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "DeviceStatus",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStatus_Text",
                table: "DeviceStatus",
                column: "Text",
                unique: true);
        }
    }
}
