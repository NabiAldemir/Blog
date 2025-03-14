using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class release_relation_blog_writer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Writers_WriterID1",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_WriterID1",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "WriterID1",
                table: "Blogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterID1",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_WriterID1",
                table: "Blogs",
                column: "WriterID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Writers_WriterID1",
                table: "Blogs",
                column: "WriterID1",
                principalTable: "Writers",
                principalColumn: "WriterID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
