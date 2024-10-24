using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDNetCore.Migrations
{
    /// <inheritdoc />
    public partial class PendingChangesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "PendingChanges");

            migrationBuilder.CreateIndex(
                name: "IX_PendingChanges_UserId",
                table: "PendingChanges",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PendingChanges_UserId",
                table: "PendingChanges");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "PendingChanges",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
