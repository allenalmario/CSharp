using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAccounts.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Transactions",
                type: "double",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
