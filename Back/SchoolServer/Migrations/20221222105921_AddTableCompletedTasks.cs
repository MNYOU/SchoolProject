using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolServer.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCompletedTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usersTasks",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    UserDalId = table.Column<long>(type: "bigint", nullable: false),
                    taskId = table.Column<int>(type: "integer", nullable: false),
                    TaskDalId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_usersTasks_tasks_TaskDalId",
                        column: x => x.TaskDalId,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usersTasks_users_UserDalId",
                        column: x => x.UserDalId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usersTasks_TaskDalId",
                table: "usersTasks",
                column: "TaskDalId");

            migrationBuilder.CreateIndex(
                name: "IX_usersTasks_UserDalId",
                table: "usersTasks",
                column: "UserDalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usersTasks");
        }
    }
}
