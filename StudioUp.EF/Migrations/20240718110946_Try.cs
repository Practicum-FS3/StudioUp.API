using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class Try : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "T_SubscriptionTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTrainingPerWeek",
                table: "T_SubscriptionTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceForTraining",
                table: "T_SubscriptionTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTraining",
                table: "T_SubscriptionTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "T_SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "NumberOfTrainingPerWeek",
                table: "T_SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "PriceForTraining",
                table: "T_SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "TotalTraining",
                table: "T_SubscriptionTypes");
        }
    }
}
