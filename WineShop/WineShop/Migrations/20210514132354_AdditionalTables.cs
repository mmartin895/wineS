using Microsoft.EntityFrameworkCore.Migrations;

namespace WineShop.Migrations
{
    public partial class AdditionalTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WineTypeId",
                table: "Wines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageTypeId",
                table: "Packages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WineType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wines_WineTypeId",
                table: "Wines",
                column: "WineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_PackageTypeId",
                table: "Packages",
                column: "PackageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageTypes_PackageTypeId",
                table: "Packages",
                column: "PackageTypeId",
                principalTable: "PackageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wines_WineType_WineTypeId",
                table: "Wines",
                column: "WineTypeId",
                principalTable: "WineType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageTypes_PackageTypeId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Wines_WineType_WineTypeId",
                table: "Wines");

            migrationBuilder.DropTable(
                name: "PackageTypes");

            migrationBuilder.DropTable(
                name: "WineType");

            migrationBuilder.DropIndex(
                name: "IX_Wines_WineTypeId",
                table: "Wines");

            migrationBuilder.DropIndex(
                name: "IX_Packages_PackageTypeId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "WineTypeId",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "PackageTypeId",
                table: "Packages");
        }
    }
}
