using Microsoft.EntityFrameworkCore.Migrations;

namespace WineShop.Migrations
{
    public partial class initialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WineType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Dobro vino", "Chardonay" });

            migrationBuilder.InsertData(
                table: "WineType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Dobro vino1", "Vino tipa 2" });

            migrationBuilder.InsertData(
                table: "WineType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Dobro vino2", "Vino tipa 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WineType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WineType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WineType",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
