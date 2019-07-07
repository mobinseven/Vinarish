using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VinarishMvc.Migrations
{
    public partial class Assistant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wagons_Number",
                table: "Wagons");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Wagons");

            migrationBuilder.AlterColumn<int>(
                name: "ReporterId",
                table: "TrainTrips",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "Reports",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Assistants",
                columns: table => new
                {
                    AssistantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    ReportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assistants", x => x.AssistantId);
                    table.ForeignKey(
                        name: "FK_Assistants_Reporters_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Reporters",
                        principalColumn: "ReporterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assistants_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assistants_PersonId",
                table: "Assistants",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Assistants_ReportId",
                table: "Assistants",
                column: "ReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assistants");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Wagons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ReporterId",
                table: "TrainTrips",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wagons_Number",
                table: "Wagons",
                column: "Number",
                unique: true);
        }
    }
}
