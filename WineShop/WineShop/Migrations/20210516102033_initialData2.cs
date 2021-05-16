using Microsoft.EntityFrameworkCore.Migrations;

namespace WineShop.Migrations
{
    public partial class initialData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PackageTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Dobrotvorni" });

            migrationBuilder.InsertData(
                table: "PackageTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Akcijski" });

            migrationBuilder.InsertData(
                table: "PackageTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Promidzbeni" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PackageTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PackageTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PackageTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
