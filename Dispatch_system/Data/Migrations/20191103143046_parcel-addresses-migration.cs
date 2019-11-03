using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class parceladdressesmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverAddress",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ReceiverCity",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "ReceiverPostalCode",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "SenderAddress",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "SenderCity",
                table: "Parcels");

            migrationBuilder.RenameColumn(
                name: "SenderPostalCode",
                table: "Parcels",
                newName: "ParcelAddressesId");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Parcels",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "Parcels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ParcelsAddresses",
                columns: table => new
                {
                    ParcelAddressesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SenderAddress = table.Column<string>(nullable: false),
                    SenderPostalCode = table.Column<int>(nullable: false),
                    SenderCity = table.Column<string>(nullable: false),
                    ReceiverAddress = table.Column<string>(nullable: false),
                    ReceiverPostalCode = table.Column<int>(nullable: false),
                    ReceiverCity = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelsAddresses", x => x.ParcelAddressesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParcelsAddresses");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "Parcels");

            migrationBuilder.RenameColumn(
                name: "ParcelAddressesId",
                table: "Parcels",
                newName: "SenderPostalCode");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Parcels",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverAddress",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverCity",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverPostalCode",
                table: "Parcels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SenderAddress",
                table: "Parcels",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderCity",
                table: "Parcels",
                nullable: false,
                defaultValue: "");
        }
    }
}
