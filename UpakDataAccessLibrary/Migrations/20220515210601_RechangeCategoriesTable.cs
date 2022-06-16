using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpakDataAccessLibrary.Migrations
{
    public partial class RechangeCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMasterCategory",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MasterCategoryName",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "MasterCategoryId",
                table: "Categories",
                newName: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                table: "Categories",
                newName: "MasterCategoryId");

            migrationBuilder.AddColumn<bool>(
                name: "IsMasterCategory",
                table: "Categories",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MasterCategoryName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
