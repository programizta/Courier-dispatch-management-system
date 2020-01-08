using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class visible_for_courier_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "Parcels");

            migrationBuilder.AddColumn<bool>(
                name: "VisibleForCourier",
                table: "Parcels",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisibleForCourier",
                table: "Parcels");

            migrationBuilder.AddColumn<int>(
                name: "Insurance",
                table: "Parcels",
                nullable: true);
        }
    }
}
