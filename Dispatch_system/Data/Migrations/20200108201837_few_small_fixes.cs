using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class few_small_fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "BranchId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}
