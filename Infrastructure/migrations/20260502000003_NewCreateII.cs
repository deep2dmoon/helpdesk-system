using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.infrastructure.migrations
{
    /// <inheritdoc />
    public partial class NewCreateII : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketChats_ConversationID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ConversationID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ConversationID",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "TicketID",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TicketID",
                table: "Messages",
                column: "TicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Tickets_TicketID",
                table: "Messages",
                column: "TicketID",
                principalTable: "Tickets",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Tickets_TicketID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_TicketID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "TicketID",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "ConversationID",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ConversationID",
                table: "Tickets",
                column: "ConversationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketChats_ConversationID",
                table: "Tickets",
                column: "ConversationID",
                principalTable: "TicketChats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
