using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace itec420.Migrations
{
    /// <inheritdoc />
    public partial class MakeAppUserIdNullableInDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Doctors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3,
                column: "AppUserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AppUserId",
                table: "Doctors",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_AppUserId",
                table: "Doctors",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_AppUserId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AppUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Doctors");
        }
    }
}
