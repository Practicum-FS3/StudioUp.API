using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHMOFieldsfinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArrangementName",
                table: "T_HMOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "MaximumAge",
                table: "T_HMOs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinimumAge",
                table: "T_HMOs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TrainingDescription",
                table: "T_HMOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TrainingPrice",
                table: "T_HMOs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TrainingsPerMonth",
                table: "T_HMOs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrangementName",
                table: "T_HMOs");

            migrationBuilder.DropColumn(
                name: "MaximumAge",
                table: "T_HMOs");

            migrationBuilder.DropColumn(
                name: "MinimumAge",
                table: "T_HMOs");

            migrationBuilder.DropColumn(
                name: "TrainingDescription",
                table: "T_HMOs");

            migrationBuilder.DropColumn(
                name: "TrainingPrice",
                table: "T_HMOs");

            migrationBuilder.DropColumn(
                name: "TrainingsPerMonth",
                table: "T_HMOs");
        }
    }
}
