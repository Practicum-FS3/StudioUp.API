using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class IsActiveInTCT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "T_TrainingCustomerTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_TrainingCustomerTypes");
        }
    }
}
