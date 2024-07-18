using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioUp.Models.Migrations
{
    /// <inheritdoc />
    public partial class _1001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_TrainingCustomersTypes_T_CustomerTypes_CustomerTypeID",
            //    table: "TrainingCustomersTypes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TrainingCustomersTypes_T_TrainigTypes_TrainingTypeId",
            //    table: "TrainingCustomersTypes");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_TrainingCustomersTypes",
            //    table: "TrainingCustomersTypes");

            //migrationBuilder.RenameTable(
            //    name: "TrainingCustomersTypes",
            //    newName: "T_TrainingCustomerTypes");

            //migrationBuilder.RenameIndex(
            //    name: "IX_TrainingCustomersTypes_TrainingTypeId",
            //    table: "T_TrainingCustomerTypes",
            //    newName: "IX_T_TrainingCustomerTypes_TrainingTypeId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_TrainingCustomersTypes_CustomerTypeID",
            //    table: "T_TrainingCustomerTypes",
            //    newName: "IX_T_TrainingCustomerTypes_CustomerTypeID");

            //migrationBuilder.AddColumn<string>(
            //    name: "Email",
            //    table: "T_Customers",
            //    type: "nvarchar(50)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_T_TrainingCustomerTypes",
            //    table: "T_TrainingCustomerTypes",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_T_TrainingCustomerTypes_T_CustomerTypes_CustomerTypeID",
            //    table: "T_TrainingCustomerTypes",
            //    column: "CustomerTypeID",
            //    principalTable: "T_CustomerTypes",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_T_TrainingCustomerTypes_T_TrainigTypes_TrainingTypeId",
            //    table: "T_TrainingCustomerTypes",
            //    column: "TrainingTypeId",
            //    principalTable: "T_TrainigTypes",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_T_TrainingCustomerTypes_T_CustomerTypes_CustomerTypeID",
            //    table: "T_TrainingCustomerTypes");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_T_TrainingCustomerTypes_T_TrainigTypes_TrainingTypeId",
            //    table: "T_TrainingCustomerTypes");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_T_TrainingCustomerTypes",
            //    table: "T_TrainingCustomerTypes");

            //migrationBuilder.DropColumn(
            //    name: "Email",
            //    table: "T_Customers");

            //migrationBuilder.RenameTable(
            //    name: "T_TrainingCustomerTypes",
            //    newName: "TrainingCustomersTypes");

            //migrationBuilder.RenameIndex(
            //    name: "IX_T_TrainingCustomerTypes_TrainingTypeId",
            //    table: "TrainingCustomersTypes",
            //    newName: "IX_TrainingCustomersTypes_TrainingTypeId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_T_TrainingCustomerTypes_CustomerTypeID",
            //    table: "TrainingCustomersTypes",
            //    newName: "IX_TrainingCustomersTypes_CustomerTypeID");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_TrainingCustomersTypes",
            //    table: "TrainingCustomersTypes",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TrainingCustomersTypes_T_CustomerTypes_CustomerTypeID",
            //    table: "TrainingCustomersTypes",
            //    column: "CustomerTypeID",
            //    principalTable: "T_CustomerTypes",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TrainingCustomersTypes_T_TrainigTypes_TrainingTypeId",
            //    table: "TrainingCustomersTypes",
            //    column: "TrainingTypeId",
            //    principalTable: "T_TrainigTypes",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
