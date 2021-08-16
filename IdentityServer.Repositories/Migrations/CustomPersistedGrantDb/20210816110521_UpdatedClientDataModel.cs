using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Repositories.Migrations.CustomPersistedGrantDb
{
    public partial class UpdatedClientDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoothId",
                table: "ClientData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrespondentCode",
                table: "ClientData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmUserBelongsTo",
                table: "ClientData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Office",
                table: "ClientData",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoothId",
                table: "ClientData");

            migrationBuilder.DropColumn(
                name: "CorrespondentCode",
                table: "ClientData");

            migrationBuilder.DropColumn(
                name: "FirmUserBelongsTo",
                table: "ClientData");

            migrationBuilder.DropColumn(
                name: "Office",
                table: "ClientData");
        }
    }
}
