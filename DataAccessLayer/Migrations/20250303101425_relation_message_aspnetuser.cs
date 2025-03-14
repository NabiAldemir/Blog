using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class relation_message_aspnetuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_Writers_ReceiverID",
                table: "Messages2");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_Writers_SenderID",
                table: "Messages2");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_AspNetUsers_ReceiverID",
                table: "Messages2",
                column: "ReceiverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_AspNetUsers_SenderID",
                table: "Messages2",
                column: "SenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_AspNetUsers_ReceiverID",
                table: "Messages2");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages2_AspNetUsers_SenderID",
                table: "Messages2");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_Writers_ReceiverID",
                table: "Messages2",
                column: "ReceiverID",
                principalTable: "Writers",
                principalColumn: "WriterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages2_Writers_SenderID",
                table: "Messages2",
                column: "SenderID",
                principalTable: "Writers",
                principalColumn: "WriterID");
        }
    }
}
