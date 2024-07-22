using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class Try2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentSections",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentTypeID = table.Column<int>(type: "int", nullable: false),
                    Section1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ViewInHP = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentSections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContentSections_ContentTypes_ContentTypeID",
                        column: x => x.ContentTypeID,
                        principalTable: "ContentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentSections_ContentTypeID",
                table: "ContentSections",
                column: "ContentTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentSections");
        }
    }
}
