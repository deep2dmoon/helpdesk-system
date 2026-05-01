using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.infrastructure.migrations
{
    /// <inheritdoc />
    public partial class newCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CategoryID",
                table: "Tickets",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketCategories_CategoryID",
                table: "Tickets",
                column: "CategoryID",
                principalTable: "TicketCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketCategories_CategoryID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CategoryID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
