using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPostingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posting",
                columns: table => new
                {
                    PostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionFlag = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posting", x => x.PostingId);
                });

            migrationBuilder.CreateTable(
                name: "PostingDetails",
                columns: table => new
                {
                    PostingDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostingId = table.Column<int>(type: "int", nullable: false),
                    LikeStatus = table.Column<int>(type: "int", nullable: true),
                    ShareStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostingDetails", x => x.PostingDetailsId);
                    table.ForeignKey(
                        name: "FK_PostingDetails_Posting_PostingId",
                        column: x => x.PostingId,
                        principalTable: "Posting",
                        principalColumn: "PostingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostingDetails_PostingId",
                table: "PostingDetails",
                column: "PostingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostingDetails");

            migrationBuilder.DropTable(
                name: "Posting");
        }
    }
}
