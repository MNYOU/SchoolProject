using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolServer.Migrations
{
    /// <inheritdoc />
    public partial class LinkMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usersTasks_tasks_TaskDalId",
                table: "usersTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_usersTasks_users_UserDalId",
                table: "usersTasks");

            migrationBuilder.DropIndex(
                name: "IX_usersTasks_TaskDalId",
                table: "usersTasks");

            migrationBuilder.DropIndex(
                name: "IX_usersTasks_UserDalId",
                table: "usersTasks");

            migrationBuilder.DropColumn(
                name: "TaskDalId",
                table: "usersTasks");

            migrationBuilder.DropColumn(
                name: "UserDalId",
                table: "usersTasks");

            migrationBuilder.AlterColumn<long>(
                name: "userId",
                table: "usersTasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "taskId",
                table: "usersTasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "TaskDalUserDal",
                columns: table => new
                {
                    CompletedTasksId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDalUserDal", x => new { x.CompletedTasksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TaskDalUserDal_tasks_CompletedTasksId",
                        column: x => x.CompletedTasksId,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskDalUserDal_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usersTasks_taskId",
                table: "usersTasks",
                column: "taskId");

            migrationBuilder.CreateIndex(
                name: "IX_usersTasks_userId",
                table: "usersTasks",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDalUserDal_UsersId",
                table: "TaskDalUserDal",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_usersTasks_tasks_taskId",
                table: "usersTasks",
                column: "taskId",
                principalTable: "tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usersTasks_users_userId",
                table: "usersTasks",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usersTasks_tasks_taskId",
                table: "usersTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_usersTasks_users_userId",
                table: "usersTasks");

            migrationBuilder.DropTable(
                name: "TaskDalUserDal");

            migrationBuilder.DropIndex(
                name: "IX_usersTasks_taskId",
                table: "usersTasks");

            migrationBuilder.DropIndex(
                name: "IX_usersTasks_userId",
                table: "usersTasks");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "usersTasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "taskId",
                table: "usersTasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "TaskDalId",
                table: "usersTasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserDalId",
                table: "usersTasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_usersTasks_TaskDalId",
                table: "usersTasks",
                column: "TaskDalId");

            migrationBuilder.CreateIndex(
                name: "IX_usersTasks_UserDalId",
                table: "usersTasks",
                column: "UserDalId");

            migrationBuilder.AddForeignKey(
                name: "FK_usersTasks_tasks_TaskDalId",
                table: "usersTasks",
                column: "TaskDalId",
                principalTable: "tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usersTasks_users_UserDalId",
                table: "usersTasks",
                column: "UserDalId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
