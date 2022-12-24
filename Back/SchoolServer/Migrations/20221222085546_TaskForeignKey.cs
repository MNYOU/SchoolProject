using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolServer.Migrations
{
    /// <inheritdoc />
    public partial class TaskForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DifficultyDalId",
                table: "tasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_DifficultyDalId",
                table: "tasks",
                column: "DifficultyDalId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_difficulties_DifficultyDalId",
                table: "tasks",
                column: "DifficultyDalId",
                principalTable: "difficulties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_difficulties_DifficultyDalId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_DifficultyDalId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "DifficultyDalId",
                table: "tasks");
        }
    }
}
