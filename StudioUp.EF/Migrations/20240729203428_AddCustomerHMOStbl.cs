using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerHMOStbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "ContentTypes",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<byte[]>(
            //    name: "ImageData",
            //    table: "ContentSections",
            //    type: "varbinary(max)",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "Files",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
            //        ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Files", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "T_CustomerHMOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    HMOID = table.Column<int>(type: "int", nullable: false),
                    FreeFitId = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CustomerHMOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_CustomerHMOS_T_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "T_Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_CustomerHMOS_CustomerID",
                table: "T_CustomerHMOS",
                column: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Files");

            migrationBuilder.DropTable(
                name: "T_CustomerHMOS");

            //migrationBuilder.DropColumn(
            //    name: "IsActive",
            //    table: "ContentTypes");

            //migrationBuilder.DropColumn(
            //    name: "ImageData",
            //    table: "ContentSections");
        }
    }
}
