using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.REPO.Migrations
{
    public partial class dsdfs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "FeePerAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount1",
                table: "FeePerAmount",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount2",
                table: "FeePerAmount",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount1",
                table: "FeePerAmount");

            migrationBuilder.DropColumn(
                name: "Amount2",
                table: "FeePerAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "FeePerAmount",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
