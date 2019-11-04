using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class parceldenormalisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParcelsAddresses");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Parcels");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Parcels",
                newName: "SenderBranchId");

            migrationBuilder.RenameColumn(
                name: "ParcelAddressesId",
                table: "Parcels",
                newName: "SenderFlatNumber");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Parcels",
                newName: "ReceiverBranchId");

            migrationBuilder.AddColumn<short>(
                name: "ParcelStatusId",
                table: "Parcels",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Parcels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverBlockNumber",
                table: "Parcels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverCity",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverFlatNumber",
                table: "Parcels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverPostalCode",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverStreetName",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SenderBlockNumber",
                table: "Parcels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SenderCity",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderPostalCode",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderStreetName",
                table: "Parcels",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParcelStatusId",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ReceiverBlockNumber",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ReceiverCity",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ReceiverFlatNumber",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ReceiverPostalCode",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ReceiverStreetName",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "SenderBlockNumber",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "SenderCity",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "SenderPostalCode",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "SenderStreetName",
                table: "Parcels");

            migrationBuilder.RenameColumn(
                name: "SenderFlatNumber",
                table: "Parcels",
                newName: "ParcelAddressesId");

            migrationBuilder.RenameColumn(
                name: "SenderBranchId",
                table: "Parcels",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "ReceiverBranchId",
                table: "Parcels",
                newName: "BranchId");

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Parcels",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ParcelsAddresses",
                columns: table => new
                {
                    ParcelAddressesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceiverAddress = table.Column<string>(nullable: false),
                    ReceiverCity = table.Column<string>(nullable: false),
                    ReceiverPostalCode = table.Column<string>(nullable: false),
                    SenderAddress = table.Column<string>(nullable: false),
                    SenderCity = table.Column<string>(nullable: false),
                    SenderPostalCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelsAddresses", x => x.ParcelAddressesId);
                });
        }
    }
}
