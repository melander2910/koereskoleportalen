using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackOffice.API.Migrations
{
    /// <inheritdoc />
    public partial class SpecifyingTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionUnitUser_Tenant_ProductionUnitsId",
                table: "ProductionUnitUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTenant_Tenant_ProductionUnitId",
                table: "SubTenant");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_Organisations_OrganisationId",
                table: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_Tenant_OrganisationId",
                table: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_SubTenant_ProductionUnitId",
                table: "SubTenant");

            migrationBuilder.DropColumn(
                name: "CVR",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "CvrApiModifiedDate",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "IndustryCode",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "IndustryDescription",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "ProductionUnitNumber",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "SubTenant");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubTenant");

            migrationBuilder.DropColumn(
                name: "ProductionUnitId",
                table: "SubTenant");

            migrationBuilder.CreateTable(
                name: "ProductionUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductionUnitNumber = table.Column<string>(type: "text", nullable: false),
                    CVR = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CvrApiModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OrganisationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Municipality = table.Column<string>(type: "text", nullable: true),
                    IndustryCode = table.Column<string>(type: "text", nullable: true),
                    IndustryDescription = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    StreetAddress = table.Column<string>(type: "text", nullable: true),
                    Zipcode = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longtitude = table.Column<double>(type: "double precision", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionUnits_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionUnits_Tenant_Id",
                        column: x => x.Id,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductionUnitId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_ProductionUnits_ProductionUnitId",
                        column: x => x.ProductionUnitId,
                        principalTable: "ProductionUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_SubTenant_Id",
                        column: x => x.Id,
                        principalTable: "SubTenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProductionUnitId",
                table: "Courses",
                column: "ProductionUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionUnits_OrganisationId",
                table: "ProductionUnits",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionUnitUser_ProductionUnits_ProductionUnitsId",
                table: "ProductionUnitUser",
                column: "ProductionUnitsId",
                principalTable: "ProductionUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionUnitUser_ProductionUnits_ProductionUnitsId",
                table: "ProductionUnitUser");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "ProductionUnits");

            migrationBuilder.AddColumn<string>(
                name: "CVR",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CvrApiModifiedDate",
                table: "Tenant",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tenant",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Tenant",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryCode",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryDescription",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Tenant",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "Tenant",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganisationId",
                table: "Tenant",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionUnitNumber",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Tenant",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                table: "Tenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "SubTenant",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubTenant",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductionUnitId",
                table: "SubTenant",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_OrganisationId",
                table: "Tenant",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTenant_ProductionUnitId",
                table: "SubTenant",
                column: "ProductionUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionUnitUser_Tenant_ProductionUnitsId",
                table: "ProductionUnitUser",
                column: "ProductionUnitsId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTenant_Tenant_ProductionUnitId",
                table: "SubTenant",
                column: "ProductionUnitId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_Organisations_OrganisationId",
                table: "Tenant",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
