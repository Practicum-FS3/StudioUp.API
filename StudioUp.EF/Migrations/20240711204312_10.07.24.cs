using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class _100724 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CustomerTypeId = table.Column<int>(type: "int", nullable: false),
                    HMOId = table.Column<int>(type: "int", nullable: false),
                    PaymentOptionsId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_CustomerTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CustomerTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_PaymentOptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PaymentOptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_SubscriptionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SubscriptionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_Trainers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Trainers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_TrainigTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TrainigTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_TrainigTypes_T_CustomerTypes_CustomerTypeID",
                        column: x => x.CustomerTypeID,
                        principalTable: "T_CustomerTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Trainings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTypeID = table.Column<int>(type: "int", nullable: false),
                    TrainerID = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<TimeOnly>(type: "time", nullable: false),
                    ParticipantsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Trainings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_Trainings_T_Trainers_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "T_Trainers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Trainings_T_TrainigTypes_TrainingTypeID",
                        column: x => x.TrainingTypeID,
                        principalTable: "T_TrainigTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AvailableTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ParticipantsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AvailableTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_AvailableTrainings_T_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "T_Trainings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_TrainingsCustomers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TrainingsCustomers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_T_TrainingsCustomers_T_AvailableTrainings_TrainingID",
                        column: x => x.TrainingID,
                        principalTable: "T_AvailableTrainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_TrainingsCustomers_T_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "T_Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_AvailableTrainings_TrainingId",
                table: "T_AvailableTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainigTypes_CustomerTypeID",
                table: "T_TrainigTypes",
                column: "CustomerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_T_Trainings_TrainerID",
                table: "T_Trainings",
                column: "TrainerID");

            migrationBuilder.CreateIndex(
                name: "IX_T_Trainings_TrainingTypeID",
                table: "T_Trainings",
                column: "TrainingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingsCustomers_CustomerID",
                table: "T_TrainingsCustomers",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingsCustomers_TrainingID",
                table: "T_TrainingsCustomers",
                column: "TrainingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_PaymentOptions");

            migrationBuilder.DropTable(
                name: "T_SubscriptionTypes");

            migrationBuilder.DropTable(
                name: "T_TrainingsCustomers");

            migrationBuilder.DropTable(
                name: "T_AvailableTrainings");

            migrationBuilder.DropTable(
                name: "T_Customers");

            migrationBuilder.DropTable(
                name: "T_Trainings");

            migrationBuilder.DropTable(
                name: "T_Trainers");

            migrationBuilder.DropTable(
                name: "T_TrainigTypes");

            migrationBuilder.DropTable(
                name: "T_CustomerTypes");
        }
    }
}
