using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolServer.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "subjectId",
                table: "tasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_subjectId",
                table: "tasks",
                column: "subjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_subjects_subjectId",
                table: "tasks",
                column: "subjectId",
                principalTable: "subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_subjects_subjectId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_subjectId",
                table: "tasks");

            migrationBuilder.AlterColumn<int>(
                name: "subjectId",
                table: "tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
