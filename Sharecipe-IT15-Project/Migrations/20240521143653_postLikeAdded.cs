using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sharecipe_IT15_Project.Migrations
{
    /// <inheritdoc />
    public partial class postLikeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "postLikes",
                table: "Posts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "postLikes",
                table: "Posts");
        }
    }
}
