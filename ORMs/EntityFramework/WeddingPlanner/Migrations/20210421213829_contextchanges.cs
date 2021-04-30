using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class contextchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeddingGuest_Users_UserId",
                table: "UserWeddingGuest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWeddingGuest_Weddings_WeddingId",
                table: "UserWeddingGuest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWeddingGuest",
                table: "UserWeddingGuest");

            migrationBuilder.RenameTable(
                name: "UserWeddingGuest",
                newName: "UserWeddingGuests");

            migrationBuilder.RenameIndex(
                name: "IX_UserWeddingGuest_WeddingId",
                table: "UserWeddingGuests",
                newName: "IX_UserWeddingGuests_WeddingId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWeddingGuest_UserId",
                table: "UserWeddingGuests",
                newName: "IX_UserWeddingGuests_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWeddingGuests",
                table: "UserWeddingGuests",
                column: "UserWeddingGuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeddingGuests_Users_UserId",
                table: "UserWeddingGuests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeddingGuests_Weddings_WeddingId",
                table: "UserWeddingGuests",
                column: "WeddingId",
                principalTable: "Weddings",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeddingGuests_Users_UserId",
                table: "UserWeddingGuests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWeddingGuests_Weddings_WeddingId",
                table: "UserWeddingGuests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWeddingGuests",
                table: "UserWeddingGuests");

            migrationBuilder.RenameTable(
                name: "UserWeddingGuests",
                newName: "UserWeddingGuest");

            migrationBuilder.RenameIndex(
                name: "IX_UserWeddingGuests_WeddingId",
                table: "UserWeddingGuest",
                newName: "IX_UserWeddingGuest_WeddingId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWeddingGuests_UserId",
                table: "UserWeddingGuest",
                newName: "IX_UserWeddingGuest_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWeddingGuest",
                table: "UserWeddingGuest",
                column: "UserWeddingGuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeddingGuest_Users_UserId",
                table: "UserWeddingGuest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeddingGuest_Weddings_WeddingId",
                table: "UserWeddingGuest",
                column: "WeddingId",
                principalTable: "Weddings",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
