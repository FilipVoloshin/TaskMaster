using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserIdToAssigneeIdInAssignedTaskListTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assigned_task_lists_users_user_id",
                table: "assigned_task_lists");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "assigned_task_lists",
                newName: "assignee_id");

            migrationBuilder.RenameIndex(
                name: "ix_assigned_task_lists_user_id_task_list_id",
                table: "assigned_task_lists",
                newName: "ix_assigned_task_lists_assignee_id_task_list_id");

            migrationBuilder.AddForeignKey(
                name: "fk_assigned_task_lists_users_assignee_id",
                table: "assigned_task_lists",
                column: "assignee_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assigned_task_lists_users_assignee_id",
                table: "assigned_task_lists");

            migrationBuilder.RenameColumn(
                name: "assignee_id",
                table: "assigned_task_lists",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_assigned_task_lists_assignee_id_task_list_id",
                table: "assigned_task_lists",
                newName: "ix_assigned_task_lists_user_id_task_list_id");

            migrationBuilder.AddForeignKey(
                name: "fk_assigned_task_lists_users_user_id",
                table: "assigned_task_lists",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
