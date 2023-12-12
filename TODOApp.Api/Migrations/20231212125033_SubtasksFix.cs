using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODOApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class SubtasksFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtask_UserTasks_UserTaskID",
                table: "Subtask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtask",
                table: "Subtask");

            migrationBuilder.RenameTable(
                name: "Subtask",
                newName: "Subtasks");

            migrationBuilder.RenameIndex(
                name: "IX_Subtask_UserTaskID",
                table: "Subtasks",
                newName: "IX_Subtasks_UserTaskID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtasks",
                table: "Subtasks",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtasks_UserTasks_UserTaskID",
                table: "Subtasks",
                column: "UserTaskID",
                principalTable: "UserTasks",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtasks_UserTasks_UserTaskID",
                table: "Subtasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subtasks",
                table: "Subtasks");

            migrationBuilder.RenameTable(
                name: "Subtasks",
                newName: "Subtask");

            migrationBuilder.RenameIndex(
                name: "IX_Subtasks_UserTaskID",
                table: "Subtask",
                newName: "IX_Subtask_UserTaskID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subtask",
                table: "Subtask",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtask_UserTasks_UserTaskID",
                table: "Subtask",
                column: "UserTaskID",
                principalTable: "UserTasks",
                principalColumn: "ID");
        }
    }
}
