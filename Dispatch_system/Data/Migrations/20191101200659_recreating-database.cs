using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class recreatingdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branches_BranchId1",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchId1",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "BranchId1",
                table: "Employees",
                newName: "BranchId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Branches",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Employees",
                newName: "BranchId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId1",
                table: "Employees",
                column: "BranchId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branches_BranchId1",
                table: "Employees",
                column: "BranchId1",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
