using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddUsersTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "ApplicationTasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "ApplicationTasks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTasks_applicationUserId",
                table: "ApplicationTasks",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationTasks_AspNetUsers_applicationUserId",
                table: "ApplicationTasks",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationTasks_AspNetUsers_applicationUserId",
                table: "ApplicationTasks");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationTasks_applicationUserId",
                table: "ApplicationTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "ApplicationTasks");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "ApplicationTasks");
        }
    }
}
