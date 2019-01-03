using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModeBureauDB.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomingTask",
                columns: table => new
                {
                    IncomingTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Costumer = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    NumberOfDays = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    NumberOfModels = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingTask", x => x.IncomingTaskId);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    modelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    HairColour = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.modelId);
                });

            migrationBuilder.CreateTable(
                name: "ModelIncomingTask",
                columns: table => new
                {
                    ModelId = table.Column<int>(nullable: false),
                    IncomingTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelIncomingTask", x => new { x.IncomingTaskId, x.ModelId });
                    table.ForeignKey(
                        name: "FK_ModelIncomingTask_IncomingTask_IncomingTaskId",
                        column: x => x.IncomingTaskId,
                        principalTable: "IncomingTask",
                        principalColumn: "IncomingTaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelIncomingTask_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "modelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelIncomingTask_ModelId",
                table: "ModelIncomingTask",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelIncomingTask");

            migrationBuilder.DropTable(
                name: "IncomingTask");

            migrationBuilder.DropTable(
                name: "Model");
        }
    }
}
