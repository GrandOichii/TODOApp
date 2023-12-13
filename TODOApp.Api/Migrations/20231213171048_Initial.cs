using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TODOApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserTasks_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Subtasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    OwnerTaskID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Subtasks_UserTasks_OwnerTaskID",
                        column: x => x.OwnerTaskID,
                        principalTable: "UserTasks",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subtasks_OwnerTaskID",
                table: "Subtasks",
                column: "OwnerTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_Username",
                table: "UserTasks",
                column: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subtasks");

            migrationBuilder.DropTable(
                name: "UserTasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
