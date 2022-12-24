using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolServer.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "difficultyId",
                table: "tasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_difficultyId",
                table: "tasks",
                column: "difficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_difficulties_difficultyId",
                table: "tasks",
                column: "difficultyId",
                principalTable: "difficulties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_difficulties_difficultyId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_difficultyId",
                table: "tasks");

            migrationBuilder.AlterColumn<int>(
                name: "difficultyId",
                table: "tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
