using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class databaserebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "People");

            migrationBuilder.DropColumn(
                name: "City",
                table: "People");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "SenderBranchId",
                table: "Parcels",
                newName: "TargetBranchId");

            migrationBuilder.RenameColumn(
                name: "ReceiverBranchId",
                table: "Parcels",
                newName: "LastBranchId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Parcels",
                newName: "CourierId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Parcels",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCourier",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    UserAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: false),
                    FlatNumber = table.Column<int>(nullable: false),
                    BlockNumber = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.UserAddressId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "IsCourier",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TargetBranchId",
                table: "Parcels",
                newName: "SenderBranchId");

            migrationBuilder.RenameColumn(
                name: "LastBranchId",
                table: "Parcels",
                newName: "ReceiverBranchId");

            migrationBuilder.RenameColumn(
                name: "CourierId",
                table: "Parcels",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "People",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "People",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "People",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Parcels",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
