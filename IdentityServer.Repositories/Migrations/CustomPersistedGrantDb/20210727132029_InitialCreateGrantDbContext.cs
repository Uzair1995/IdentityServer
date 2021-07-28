using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Repositories.Migrations.CustomPersistedGrantDb
{
    public partial class InitialCreateGrantDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientData",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    EmailTemplate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientData", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceFlowCodes",
                columns: table => new
                {
                    DeviceCode = table.Column<string>(type: "text", nullable: true),
                    UserCode = table.Column<string>(type: "text", nullable: true),
                    SubjectId = table.Column<string>(type: "text", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    SubjectId = table.Column<string>(type: "text", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientData");

            migrationBuilder.DropTable(
                name: "DeviceFlowCodes");

            migrationBuilder.DropTable(
                name: "PersistedGrants");
        }
    }
}
