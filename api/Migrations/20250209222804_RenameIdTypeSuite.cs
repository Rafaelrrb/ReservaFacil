using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class RenameIdTypeSuite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suites_SuiteTypes_IdTipoSuite",
                table: "Suites");

            migrationBuilder.RenameColumn(
                name: "IdTipoSuite",
                table: "Suites",
                newName: "IdTypeSuite");

            migrationBuilder.RenameIndex(
                name: "IX_Suites_IdTipoSuite",
                table: "Suites",
                newName: "IX_Suites_IdTypeSuite");

            migrationBuilder.AddForeignKey(
                name: "FK_Suites_SuiteTypes_IdTypeSuite",
                table: "Suites",
                column: "IdTypeSuite",
                principalTable: "SuiteTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suites_SuiteTypes_IdTypeSuite",
                table: "Suites");

            migrationBuilder.RenameColumn(
                name: "IdTypeSuite",
                table: "Suites",
                newName: "IdTipoSuite");

            migrationBuilder.RenameIndex(
                name: "IX_Suites_IdTypeSuite",
                table: "Suites",
                newName: "IX_Suites_IdTipoSuite");

            migrationBuilder.AddForeignKey(
                name: "FK_Suites_SuiteTypes_IdTipoSuite",
                table: "Suites",
                column: "IdTipoSuite",
                principalTable: "SuiteTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
