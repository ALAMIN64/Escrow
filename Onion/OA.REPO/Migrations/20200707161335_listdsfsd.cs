using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.REPO.Migrations
{
    public partial class listdsfsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImageUrl1",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductImageUrl2",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductImageUrl3",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductImageUrl4",
                table: "Transactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImageUrl1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ProductImageUrl2",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ProductImageUrl3",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ProductImageUrl4",
                table: "Transactions");
        }
    }
}
