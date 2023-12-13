using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODOApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtasks_UserTasks_OwnerTaskID",
                table: "Subtasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtasks_UserTasks_OwnerTaskID",
                table: "Subtasks",
                column: "OwnerTaskID",
                principalTable: "UserTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtasks_UserTasks_OwnerTaskID",
                table: "Subtasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtasks_UserTasks_OwnerTaskID",
                table: "Subtasks",
                column: "OwnerTaskID",
                principalTable: "UserTasks",
                principalColumn: "ID");
        }
    }
}
