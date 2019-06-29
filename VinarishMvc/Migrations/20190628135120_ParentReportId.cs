using Microsoft.EntityFrameworkCore.Migrations;

namespace VinarishMvc.Migrations
{
    public partial class ParentReportId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Reports_AppendixReportId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "AppendixReportId",
                table: "Reports",
                newName: "ParentReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_AppendixReportId",
                table: "Reports",
                newName: "IX_Reports_ParentReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Reports_ParentReportId",
                table: "Reports",
                column: "ParentReportId",
                principalTable: "Reports",
                principalColumn: "ReportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Reports_ParentReportId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "ParentReportId",
                table: "Reports",
                newName: "AppendixReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ParentReportId",
                table: "Reports",
                newName: "IX_Reports_AppendixReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Reports_AppendixReportId",
                table: "Reports",
                column: "AppendixReportId",
                principalTable: "Reports",
                principalColumn: "ReportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
