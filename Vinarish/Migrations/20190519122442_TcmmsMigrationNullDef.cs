using Microsoft.EntityFrameworkCore.Migrations;

namespace Vinarish.Migrations
{
    public partial class TcmmsMigrationNullDef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AppendixReportId",
                table: "Report",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AppendixReportId",
                table: "Report",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
