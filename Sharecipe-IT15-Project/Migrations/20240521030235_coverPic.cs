using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sharecipe_IT15_Project.Migrations
{
    /// <inheritdoc />
    public partial class coverPic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "coverPic",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coverPic",
                table: "AspNetUsers");
        }
    }
}
