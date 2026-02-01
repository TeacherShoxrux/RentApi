using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentApi.Migrations
{
    /// <inheritdoc />
    public partial class initial13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Admins_AdminId1",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "AdminId1",
                table: "Images",
                newName: "RentalOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_AdminId1",
                table: "Images",
                newName: "IX_Images_RentalOrderId");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "RentalOrders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "Images",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdminId",
                table: "Images",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Admins_AdminId",
                table: "Images",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_RentalOrders_RentalOrderId",
                table: "Images",
                column: "RentalOrderId",
                principalTable: "RentalOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Admins_AdminId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_RentalOrders_RentalOrderId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AdminId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "RentalOrders");

            migrationBuilder.RenameColumn(
                name: "RentalOrderId",
                table: "Images",
                newName: "AdminId1");

            migrationBuilder.RenameIndex(
                name: "IX_Images_RentalOrderId",
                table: "Images",
                newName: "IX_Images_AdminId1");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Images",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Admins_AdminId1",
                table: "Images",
                column: "AdminId1",
                principalTable: "Admins",
                principalColumn: "Id");
        }
    }
}
