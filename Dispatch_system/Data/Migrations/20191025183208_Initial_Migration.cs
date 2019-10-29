using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchAddress = table.Column<string>(nullable: false),
                    BranchCode = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchId);
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
                name: "ParcelStatuses",
                columns: table => new
                {
                    ParcelStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatusName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelStatuses", x => x.ParcelStatusId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    RoleIdDatabaseRoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_People_DatabaseRoles_RoleIdDatabaseRoleId",
                        column: x => x.RoleIdDatabaseRoleId,
                        principalTable: "DatabaseRoles",
                        principalColumn: "DatabaseRoleId",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "Parcels",
                columns: table => new
                {
                    ParcelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SenderAddress = table.Column<string>(nullable: false),
                    SenderPostalCode = table.Column<int>(nullable: false),
                    ReceiverAddress = table.Column<string>(nullable: false),
                    ReceiverPostalCode = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false),
                    Insurance = table.Column<int>(nullable: false),
                    StatusIdParcelStatusId = table.Column<int>(nullable: true),
                    DeliveryAttempts = table.Column<short>(nullable: false),
                    Delivered = table.Column<bool>(nullable: false),
                    PersonId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.ParcelId);
                    table.ForeignKey(
                        name: "FK_Parcels_People_PersonId1",
                        column: x => x.PersonId1,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_ParcelStatuses_StatusIdParcelStatusId",
                        column: x => x.StatusIdParcelStatusId,
                        principalTable: "ParcelStatuses",
                        principalColumn: "ParcelStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParcelHistories",
                columns: table => new
                {
                    ParcelHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParcelId1 = table.Column<int>(nullable: false),
                    StatusIdParcelStatusId = table.Column<int>(nullable: false),
                    DateOfEvent = table.Column<DateTime>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_PersonId1",
                table: "Parcels",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_StatusIdParcelStatusId",
                table: "Parcels",
                column: "StatusIdParcelStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_People_RoleIdDatabaseRoleId",
                table: "People",
                column: "RoleIdDatabaseRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "ParcelHistories");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "ParcelStatuses");

            migrationBuilder.DropTable(
                name: "DatabaseRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
