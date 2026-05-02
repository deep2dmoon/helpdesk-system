using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.infrastructure.migrations
{
    /// <inheritdoc />
    public partial class NewCreateiv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileMetaDataID",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileMetaData",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DownloadURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileMetaData", x => x.ID);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_FileMetaData_FileMetaDataID",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "FileMetaData");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_FileMetaDataID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "FileMetaDataID",
                table: "Tickets");
        }
    }
}
