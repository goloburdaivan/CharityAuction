using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RzhadBids.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Lots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lots_CategoryId",
                table: "Lots",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Categories_CategoryId",
                table: "Lots",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Categories_CategoryId",
                table: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_Lots_CategoryId",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Lots");
        }
    }
}
