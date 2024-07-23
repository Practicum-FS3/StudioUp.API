using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class onint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkHP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_CustomerTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CustomerTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_HMOs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_HMOs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "T_PaymentOptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TotalTraining = table.Column<int>(type: "int", nullable: false),
                    PriceForTraining = table.Column<int>(type: "int", nullable: false),
                    NumberOfTrainingPerWeek = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TrainigTypes", x => x.ID);
                });

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

            migrationBuilder.CreateTable(
                name: "T_Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CustomerTypeId = table.Column<int>(type: "int", nullable: false),
                    HMOId = table.Column<int>(type: "int", nullable: false),
                    PaymentOptionId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Customers_T_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "T_CustomerTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Customers_T_HMOs_HMOId",
                        column: x => x.HMOId,
                        principalTable: "T_HMOs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Customers_T_PaymentOptions_PaymentOptionId",
                        column: x => x.PaymentOptionId,
                        principalTable: "T_PaymentOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Customers_T_SubscriptionTypes_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalTable: "T_SubscriptionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_TrainingCustomerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerTypeID = table.Column<int>(type: "int", nullable: false),
                    TrainingTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TrainingCustomerTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_TrainingCustomerTypes_T_CustomerTypes_CustomerTypeID",
                        column: x => x.CustomerTypeID,
                        principalTable: "T_CustomerTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_TrainingCustomerTypes_T_TrainigTypes_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "T_TrainigTypes",
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
                    ParticipantsCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                    Attended = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_ContentSections_ContentTypeID",
                table: "ContentSections",
                column: "ContentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_T_AvailableTrainings_TrainingId",
                table: "T_AvailableTrainings",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customers_CustomerTypeId",
                table: "T_Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customers_HMOId",
                table: "T_Customers",
                column: "HMOId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customers_PaymentOptionId",
                table: "T_Customers",
                column: "PaymentOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customers_SubscriptionTypeId",
                table: "T_Customers",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingCustomerTypes_CustomerTypeID",
                table: "T_TrainingCustomerTypes",
                column: "CustomerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_T_TrainingCustomerTypes_TrainingTypeId",
                table: "T_TrainingCustomerTypes",
                column: "TrainingTypeId");

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
                name: "ContentSections");

            migrationBuilder.DropTable(
                name: "T_TrainingCustomerTypes");

            migrationBuilder.DropTable(
                name: "T_TrainingsCustomers");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropTable(
                name: "T_AvailableTrainings");

            migrationBuilder.DropTable(
                name: "T_Customers");

            migrationBuilder.DropTable(
                name: "T_Trainings");

            migrationBuilder.DropTable(
                name: "T_CustomerTypes");

            migrationBuilder.DropTable(
                name: "T_HMOs");

            migrationBuilder.DropTable(
                name: "T_PaymentOptions");

            migrationBuilder.DropTable(
                name: "T_SubscriptionTypes");

            migrationBuilder.DropTable(
                name: "T_Trainers");

            migrationBuilder.DropTable(
                name: "T_TrainigTypes");
        }
    }
}
