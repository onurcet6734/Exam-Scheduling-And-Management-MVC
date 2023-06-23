using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESandMSProject.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulings_Students_StudentId",
                table: "Schedulings");

            migrationBuilder.DropIndex(
                name: "IX_Students_LoginId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Schedulings_StudentId",
                table: "Schedulings");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Classes");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LoginId",
                table: "Students",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_StudentId",
                table: "Schedulings",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulings_Students_StudentId",
                table: "Schedulings",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulings_Students_StudentId",
                table: "Schedulings");

            migrationBuilder.DropIndex(
                name: "IX_Students_LoginId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Schedulings_StudentId",
                table: "Schedulings");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Students_LoginId",
                table: "Students",
                column: "LoginId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_StudentId",
                table: "Schedulings",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulings_Students_StudentId",
                table: "Schedulings",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
