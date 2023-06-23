using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESandMSProject.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedulings_StudentId",
                table: "Schedulings");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Schedulings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_StudentId",
                table: "Schedulings",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedulings_StudentId",
                table: "Schedulings");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Schedulings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_StudentId",
                table: "Schedulings",
                column: "StudentId",
                unique: true);
        }
    }
}
