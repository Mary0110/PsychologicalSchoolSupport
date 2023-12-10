using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsychologicalSupportPlatform.Report.API.Migrations
{
    /// <inheritdoc />
    public partial class smallchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filepath",
                table: "MonthlyReports");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "MonthlyReports",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MonthlyReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MonthlyReports");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "MonthlyReports",
                newName: "DateTime");

            migrationBuilder.AddColumn<string>(
                name: "Filepath",
                table: "MonthlyReports",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
