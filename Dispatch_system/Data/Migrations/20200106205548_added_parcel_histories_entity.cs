using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dispatch_system.Data.Migrations
{
    public partial class added_parcel_histories_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParcelHistories",
                columns: table => new
                {
                    ParcelHistoryId = table.Column<int>(nullable: false),
                    DateTime = table.Column<string>(nullable: false),
                    ParcelId = table.Column<int>(nullable: false),
                    StatusName = table.Column<string>(nullable: false),
                    BranchName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelHistories", x => x.ParcelHistoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParcelHistories");
        }
    }
}
