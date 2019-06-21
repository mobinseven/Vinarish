using Microsoft.EntityFrameworkCore.Migrations;

namespace VinarishMvc.Migrations
{
    public partial class DeviceCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_Code",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Code",
                table: "Devices",
                column: "Code",
                unique: true);
        }
    }
}
