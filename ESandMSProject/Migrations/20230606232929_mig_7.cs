using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESandMSProject.Migrations
{
    /// <inheritdoc />
    public partial class mig_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Schedulings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Schedulings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
