using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeAdminAndWarehouseOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalOrders_Admins_AdminId",
                table: "RentalOrders");

            migrationBuilder.AlterColumn<int>(
                name: "WareHouseId",
                table: "RentalOrders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "RentalOrders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalOrders_Admins_AdminId",
                table: "RentalOrders",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalOrders_Admins_AdminId",
                table: "RentalOrders");

            migrationBuilder.AlterColumn<int>(
                name: "WareHouseId",
                table: "RentalOrders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "RentalOrders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalOrders_Admins_AdminId",
                table: "RentalOrders",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
