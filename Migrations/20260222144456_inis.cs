using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentApi.Migrations
{
    /// <inheritdoc />
    public partial class inis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Equipments_EquipmentId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_EquipmentId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Images");

            migrationBuilder.AddColumn<bool>(
                name: "HasAccessories",
                table: "Equipments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Equipments",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAccessories",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Equipments");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Images",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_EquipmentId",
                table: "Images",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Equipments_EquipmentId",
                table: "Images",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id");
        }
    }
}
