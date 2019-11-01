using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class rebuilddatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_People_PersonId1",
                table: "Parcels");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "DatabaseRoles");

            migrationBuilder.DropTable(
                name: "ParcelHistories");

            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "Parcels");

            migrationBuilder.RenameColumn(
                name: "PersonId1",
                table: "Parcels",
                newName: "EmployeeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Parcels_PersonId1",
                table: "Parcels",
                newName: "IX_Parcels_EmployeeId1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "People",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId1",
                table: "Parcels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverCity",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderCity",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Branches",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Branches",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_BranchId1",
                        column: x => x.BranchId1,
                        principalTable: "Branches",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_BranchId1",
                table: "Parcels",
                column: "BranchId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId1",
                table: "Employees",
                column: "BranchId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Branches_BranchId1",
                table: "Parcels",
                column: "BranchId1",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Employees_EmployeeId1",
                table: "Parcels",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Branches_BranchId1",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Employees_EmployeeId1",
                table: "Parcels");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_BranchId1",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "BranchId1",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ReceiverCity",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "SenderCity",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "EmployeeId1",
                table: "Parcels",
                newName: "PersonId1");

            migrationBuilder.RenameIndex(
                name: "IX_Parcels_EmployeeId1",
                table: "Parcels",
                newName: "IX_Parcels_PersonId1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "People",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "Delivered",
                table: "Parcels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailAddress = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    PersonId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_People_PersonId1",
                        column: x => x.PersonId1,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DatabaseRoles",
                columns: table => new
                {
                    DatabaseRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseRoles", x => x.DatabaseRoleId);
                });

            migrationBuilder.CreateTable(
                name: "ParcelHistories",
                columns: table => new
                {
                    ParcelHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfEvent = table.Column<DateTime>(nullable: false),
                    ParcelId1 = table.Column<int>(nullable: false),
                    StatusIdParcelStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelHistories", x => x.ParcelHistoryId);
                    table.ForeignKey(
                        name: "FK_ParcelHistories_Parcels_ParcelId1",
                        column: x => x.ParcelId1,
                        principalTable: "Parcels",
                        principalColumn: "ParcelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParcelHistories_ParcelStatuses_StatusIdParcelStatusId",
                        column: x => x.StatusIdParcelStatusId,
                        principalTable: "ParcelStatuses",
                        principalColumn: "ParcelStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PersonId1",
                table: "Accounts",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelHistories_ParcelId1",
                table: "ParcelHistories",
                column: "ParcelId1");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelHistories_StatusIdParcelStatusId",
                table: "ParcelHistories",
                column: "StatusIdParcelStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_People_PersonId1",
                table: "Parcels",
                column: "PersonId1",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
