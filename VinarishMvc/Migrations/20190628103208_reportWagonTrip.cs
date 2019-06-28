using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinarishMvc.Migrations
{
    public partial class reportWagonTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WagonTripId",
                table: "Reports",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Reporters",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_WagonTripId",
                table: "Reports",
                column: "WagonTripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_WagonTrips_WagonTripId",
                table: "Reports",
                column: "WagonTripId",
                principalTable: "WagonTrips",
                principalColumn: "WagonTripId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_WagonTrips_WagonTripId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_WagonTripId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "WagonTripId",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Reporters",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
