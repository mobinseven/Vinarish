using Microsoft.EntityFrameworkCore.Migrations;

namespace Vinarish.Migrations
{
    public partial class TcmmsMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_OrderNumber",
                table: "Category");

            migrationBuilder.AlterColumn<long>(
                name: "OrderNumber",
                table: "Category",
                nullable: true,
                oldClrType: typeof(byte));

            migrationBuilder.CreateIndex(
                name: "IX_Category_OrderNumber",
                table: "Category",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_OrderNumber",
                table: "Category");

            migrationBuilder.AlterColumn<byte>(
                name: "OrderNumber",
                table: "Category",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_OrderNumber",
                table: "Category",
                column: "OrderNumber",
                unique: true);
        }
    }
}
