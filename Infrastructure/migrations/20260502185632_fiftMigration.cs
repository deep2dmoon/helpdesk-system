using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.infrastructure.migrations
{
    /// <inheritdoc />
    public partial class fiftMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_FileMetaData_FileMetaDataID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_FileMetaDataID",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileMetaData",
                table: "FileMetaData");

            migrationBuilder.DropColumn(
                name: "FileMetaDataID",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "FileMetaData",
                newName: "Files");

            migrationBuilder.AddColumn<string>(
                name: "TicketID",
                table: "Files",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_TicketID",
                table: "Files",
                column: "TicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Tickets_TicketID",
                table: "Files",
                column: "TicketID",
                principalTable: "Tickets",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Tickets_TicketID",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_TicketID",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "TicketID",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "FileMetaData");

            migrationBuilder.AddColumn<string>(
                name: "FileMetaDataID",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileMetaData",
                table: "FileMetaData",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FileMetaDataID",
                table: "Tickets",
                column: "FileMetaDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_FileMetaData_FileMetaDataID",
                table: "Tickets",
                column: "FileMetaDataID",
                principalTable: "FileMetaData",
                principalColumn: "ID");
        }
    }
}
