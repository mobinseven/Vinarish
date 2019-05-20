using Microsoft.EntityFrameworkCore.Migrations;

namespace Vinarish.Migrations
{
    public partial class TcmmsMigrationAddIsValidReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "StatCode",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Report",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatCode_Code",
                table: "StatCode",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatCode_Code",
                table: "StatCode");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Report");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "StatCode",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
