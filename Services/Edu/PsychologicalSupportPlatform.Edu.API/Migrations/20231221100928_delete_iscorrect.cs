using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsychologicalSupportPlatform.Edu.API.Migrations
{
    /// <inheritdoc />
    public partial class delete_iscorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
