using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "task_lists",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task_lists", x => x.id);
                    table.ForeignKey(
                        name: "fk_task_lists_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assigned_task_lists",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_list_id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assigned_task_lists", x => x.id);
                    table.ForeignKey(
                        name: "fk_assigned_task_lists_task_lists_task_list_id",
                        column: x => x.task_list_id,
                        principalTable: "task_lists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assigned_task_lists_users_author_id",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assigned_task_lists_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_assigned_task_lists_author_id",
                table: "assigned_task_lists",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_assigned_task_lists_task_list_id",
                table: "assigned_task_lists",
                column: "task_list_id");

            migrationBuilder.CreateIndex(
                name: "ix_assigned_task_lists_user_id_task_list_id",
                table: "assigned_task_lists",
                columns: new[] { "user_id", "task_list_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_task_lists_author_id",
                table: "task_lists",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_lists_created_at_utc",
                table: "task_lists",
                column: "created_at_utc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assigned_task_lists");

            migrationBuilder.DropTable(
                name: "task_lists");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
