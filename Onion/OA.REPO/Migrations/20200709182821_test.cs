using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.REPO.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerID",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerID",
                table: "TransactionLink",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentsUrl2",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentsUrl3",
                table: "Payment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BuyerID",
                table: "TransactionLink");

            migrationBuilder.DropColumn(
                name: "DocumentsUrl2",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "DocumentsUrl3",
                table: "Payment");
        }
    }
}
