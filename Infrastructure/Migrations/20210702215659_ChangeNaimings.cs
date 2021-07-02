using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeNaimings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "ApplicationTasks",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ApplicationTasks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "endDate",
                table: "ApplicationTasks",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ApplicationTasks",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdDateTime",
                table: "ApplicationTasks",
                newName: "CreatedDateTime");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ApplicationTasks",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "ApplicationTasks",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ApplicationTasks",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "ApplicationTasks",
                newName: "endDate");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ApplicationTasks",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "ApplicationTasks",
                newName: "createdDateTime");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ApplicationTasks",
                newName: "id");
        }
    }
}
