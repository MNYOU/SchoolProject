using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolServer.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_difficulties_DifficultyDalId",
                table: "tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_subjects_SubjectDalId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_DifficultyDalId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_SubjectDalId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "DifficultyDalId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "SubjectDalId",
                table: "tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DifficultyDalId",
                table: "tasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SubjectDalId",
                table: "tasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_DifficultyDalId",
                table: "tasks",
                column: "DifficultyDalId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_SubjectDalId",
                table: "tasks",
                column: "SubjectDalId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_difficulties_DifficultyDalId",
                table: "tasks",
                column: "DifficultyDalId",
                principalTable: "difficulties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_subjects_SubjectDalId",
                table: "tasks",
                column: "SubjectDalId",
                principalTable: "subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
