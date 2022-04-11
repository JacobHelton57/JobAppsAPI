using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobApps",
                columns: table => new
                {
                    JobAppId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApps", x => x.JobAppId);
                });

            migrationBuilder.CreateTable(
                name: "JobCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCriterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormInputs",
                columns: table => new
                {
                    FormInputId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCriteriaId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobAppId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInputs", x => x.FormInputId);
                    table.ForeignKey(
                        name: "FK_FormInputs_JobApps_JobAppId",
                        column: x => x.JobAppId,
                        principalTable: "JobApps",
                        principalColumn: "JobAppId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormInputs_JobCriterias_JobCriteriaId",
                        column: x => x.JobCriteriaId,
                        principalTable: "JobCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormInputs_JobAppId",
                table: "FormInputs",
                column: "JobAppId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInputs_JobCriteriaId",
                table: "FormInputs",
                column: "JobCriteriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormInputs");

            migrationBuilder.DropTable(
                name: "JobApps");

            migrationBuilder.DropTable(
                name: "JobCriterias");
        }
    }
}
