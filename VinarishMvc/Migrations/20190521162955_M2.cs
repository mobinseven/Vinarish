using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinarishMvc.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Wagon",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Wagon_Train",
                table: "Wagon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wagon",
                table: "Wagon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Train",
                table: "Train");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wagon",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Train",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wagon",
                table: "Wagon",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Train",
                table: "Train",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Wagon",
                table: "Report",
                column: "WagonId",
                principalTable: "Wagon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wagon_Train",
                table: "Wagon",
                column: "TrainId",
                principalTable: "Train",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Wagon",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Wagon_Train",
                table: "Wagon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wagon",
                table: "Wagon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Train",
                table: "Train");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wagon");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Train");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wagon",
                table: "Wagon",
                column: "WagonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Train",
                table: "Train",
                column: "TrainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Wagon",
                table: "Report",
                column: "WagonId",
                principalTable: "Wagon",
                principalColumn: "WagonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wagon_Train",
                table: "Wagon",
                column: "TrainId",
                principalTable: "Train",
                principalColumn: "TrainId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
