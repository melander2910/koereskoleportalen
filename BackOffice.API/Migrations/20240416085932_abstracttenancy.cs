using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackOffice.API.Migrations
{
    /// <inheritdoc />
    public partial class abstracttenancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ProductionUnits_ProductionUnitId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionUnits_Organisations_OrganisationId",
                table: "ProductionUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionUnitUser_ProductionUnits_ProductionUnitsId",
                table: "ProductionUnitUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionUnits",
                table: "ProductionUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "ProductionUnits",
                newName: "Tenant");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "SubTenant");

            migrationBuilder.RenameIndex(
                name: "IX_ProductionUnits_OrganisationId",
                table: "Tenant",
                newName: "IX_Tenant_OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ProductionUnitId",
                table: "SubTenant",
                newName: "IX_SubTenant_ProductionUnitId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductionUnitNumber",
                table: "Tenant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganisationId",
                table: "Tenant",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tenant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CVR",
                table: "Tenant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tenant",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductionUnitId",
                table: "SubTenant",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SubTenant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "SubTenant",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubTenant",
                table: "SubTenant",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubTenant",
                table: "SubTenant");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "SubTenant");

            migrationBuilder.RenameTable(
                name: "Tenant",
                newName: "ProductionUnits");

            migrationBuilder.RenameTable(
                name: "SubTenant",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_Tenant_OrganisationId",
                table: "ProductionUnits",
                newName: "IX_ProductionUnits_OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_SubTenant_ProductionUnitId",
                table: "Courses",
                newName: "IX_Courses_ProductionUnitId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductionUnitNumber",
                table: "ProductionUnits",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganisationId",
                table: "ProductionUnits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductionUnits",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CVR",
                table: "ProductionUnits",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductionUnitId",
                table: "Courses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionUnits",
                table: "ProductionUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ProductionUnits_ProductionUnitId",
                table: "Courses",
                column: "ProductionUnitId",
                principalTable: "ProductionUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionUnits_Organisations_OrganisationId",
                table: "ProductionUnits",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionUnitUser_ProductionUnits_ProductionUnitsId",
                table: "ProductionUnitUser",
                column: "ProductionUnitsId",
                principalTable: "ProductionUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
