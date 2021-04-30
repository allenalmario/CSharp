using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAccounts.Migrations
{
    public partial class AmountDataTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
