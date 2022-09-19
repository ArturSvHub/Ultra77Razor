using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpakDataAccessLibrary.Migrations
{
    public partial class fixPageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pages_ProductId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ProdictId",
                table: "Pages");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ProductId",
                table: "Pages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pages_ProductId",
                table: "Pages");

            migrationBuilder.AddColumn<int>(
                name: "ProdictId",
                table: "Pages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ProductId",
                table: "Pages",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");
        }
    }
}
