using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.REPO.Migrations
{
    public partial class buyeremail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "BuyerEmail",
                table: "Transactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerEmail",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
