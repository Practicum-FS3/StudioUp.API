using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class CustomerFixedTraining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerSubscriptionId",
                table: "T_TrainingsCustomers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingsCustomers_CustomerSubscriptionId",
                table: "T_TrainingsCustomers",
                column: "CustomerSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_TrainingsCustomers_T_CustomerSubscription_CustomerSubscriptionId",
                table: "T_TrainingsCustomers",
                column: "CustomerSubscriptionId",
                principalTable: "T_CustomerSubscription",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_TrainingsCustomers_T_CustomerSubscription_CustomerSubscriptionId",
                table: "T_TrainingsCustomers");

            migrationBuilder.DropIndex(
                name: "IX_T_TrainingsCustomers_CustomerSubscriptionId",
                table: "T_TrainingsCustomers");

            migrationBuilder.DropColumn(
                name: "CustomerSubscriptionId",
                table: "T_TrainingsCustomers");
        }
    }
}
