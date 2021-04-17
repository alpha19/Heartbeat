using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LabSystems.Domain.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipaddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Osversion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiskDrives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firmware = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LabSystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiskDrives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiskDrives_LabSystems_LabSystemId",
                        column: x => x.LabSystemId,
                        principalTable: "LabSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiskDrives_LabSystemId",
                table: "DiskDrives",
                column: "LabSystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiskDrives");

            migrationBuilder.DropTable(
                name: "LabSystems");
        }
    }
}
