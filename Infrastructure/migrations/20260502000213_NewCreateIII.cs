using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.infrastructure.migrations
{
    /// <inheritdoc />
    public partial class NewCreateIII : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_TicketChats_TicketChatID",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "TicketChats");

            migrationBuilder.DropIndex(
                name: "IX_Messages_TicketChatID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "TicketChatID",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketChatID",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketChats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketChats", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TicketChatID",
                table: "Messages",
                column: "TicketChatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_TicketChats_TicketChatID",
                table: "Messages",
                column: "TicketChatID",
                principalTable: "TicketChats",
                principalColumn: "ID");
        }
    }
}
