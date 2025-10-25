using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebReddkaApi.Migrations
{
    /// <inheritdoc />
    public partial class adduserstoposts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "body",
                table: "tblPost",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "tblPost",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tblPost_user_id",
                table: "tblPost",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPost_AspNetUsers_user_id",
                table: "tblPost",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblPost_AspNetUsers_user_id",
                table: "tblPost");

            migrationBuilder.DropIndex(
                name: "IX_tblPost_user_id",
                table: "tblPost");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tblPost");

            migrationBuilder.AlterColumn<string>(
                name: "body",
                table: "tblPost",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
