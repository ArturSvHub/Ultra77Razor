using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpakDataAccessLibrary.Migrations
{
    public partial class AddPathUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePathUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePathUrl",
                table: "Products");
        }
    }
}
