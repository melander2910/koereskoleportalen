using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackOffice.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionNumber",
                table: "ProductionUnits",
                newName: "Zipcode");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ProductionUnits",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "ProductionUnits",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductionUnitNumber",
                table: "ProductionUnits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ProductionUnits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "ProductionUnits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationUser",
                columns: table => new
                {
                    OrganisationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationUser", x => new { x.OrganisationsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Organisations_OrganisationsId",
                        column: x => x.OrganisationsId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionUnitUser",
                columns: table => new
                {
                    ProductionUnitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionUnitUser", x => new { x.ProductionUnitsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ProductionUnitUser_ProductionUnits_ProductionUnitsId",
                        column: x => x.ProductionUnitsId,
                        principalTable: "ProductionUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionUnitUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_UsersId",
                table: "OrganisationUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionUnitUser_UsersId",
                table: "ProductionUnitUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganisationUser");

            migrationBuilder.DropTable(
                name: "ProductionUnitUser");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ProductionUnits");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "ProductionUnits");

            migrationBuilder.DropColumn(
                name: "ProductionUnitNumber",
                table: "ProductionUnits");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductionUnits");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "ProductionUnits");

            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "ProductionUnits",
                newName: "ProductionNumber");
        }
    }
}
