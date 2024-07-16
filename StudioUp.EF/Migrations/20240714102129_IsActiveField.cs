using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class IsActiveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "T_TrainingsCustomers",
            //    type: "bit",
            //    nullable: true,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "T_Trainings",
            //    type: "bit",
            //    nullable: true,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "T_TrainigTypes",
            //    type: "bit",
            //    nullable: true,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "T_SubscriptionTypes",
            //    type: "bit",
            //    nullable: true,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "T_PaymentOptions",
            //    type: "bit",
            //    nullable: true,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "T_CustomerTypes",
            //    type: "bit",
            //    nullable: true,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "T_AvailableTrainings",
            //    type: "bit",
            //    nullable: true,
            //    defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_TrainingsCustomers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_Trainings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_TrainigTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_SubscriptionTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_PaymentOptions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_CustomerTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "T_AvailableTrainings");
        }
    }
}
