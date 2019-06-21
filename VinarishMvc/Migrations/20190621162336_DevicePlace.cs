using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinarishMvc.Migrations
{
    public partial class DevicePlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "DeviceTypeId",
                table: "Devices",
                newName: "DevicePlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices",
                newName: "IX_Devices_DevicePlaceId");

            migrationBuilder.CreateTable(
                name: "DevicePlaces",
                columns: table => new
                {
                    DevicePlaceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DeviceTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicePlaces", x => x.DevicePlaceId);
                    table.ForeignKey(
                        name: "FK_DevicePlaces_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "DeviceTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicePlaces_Code",
                table: "DevicePlaces",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DevicePlaces_DeviceTypeId",
                table: "DevicePlaces",
                column: "DeviceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DevicePlaces_DevicePlaceId",
                table: "Devices",
                column: "DevicePlaceId",
                principalTable: "DevicePlaces",
                principalColumn: "DevicePlaceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DevicePlaces_DevicePlaceId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DevicePlaces");

            migrationBuilder.RenameColumn(
                name: "DevicePlaceId",
                table: "Devices",
                newName: "DeviceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_DevicePlaceId",
                table: "Devices",
                newName: "IX_Devices_DeviceTypeId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Devices",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Devices",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "DeviceTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
