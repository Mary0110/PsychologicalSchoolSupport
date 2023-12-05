using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsychologicalSupportPlatform.Report.API.Migrations
{
    /// <inheritdoc />
    public partial class conclusion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Conclusion",
                table: "MeetupReports",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conclusion",
                table: "MeetupReports");
        }
    }
}
