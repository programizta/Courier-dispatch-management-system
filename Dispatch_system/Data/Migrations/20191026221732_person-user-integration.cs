using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class personuserintegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_DatabaseRoles_RoleIdDatabaseRoleId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_RoleIdDatabaseRoleId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "RoleIdDatabaseRoleId",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "People",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "People",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "RoleIdDatabaseRoleId",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_RoleIdDatabaseRoleId",
                table: "People",
                column: "RoleIdDatabaseRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_DatabaseRoles_RoleIdDatabaseRoleId",
                table: "People",
                column: "RoleIdDatabaseRoleId",
                principalTable: "DatabaseRoles",
                principalColumn: "DatabaseRoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
