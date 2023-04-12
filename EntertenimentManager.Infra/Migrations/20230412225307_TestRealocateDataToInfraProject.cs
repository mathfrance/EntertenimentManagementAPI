using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntertenimentManagement.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TestRealocateDataToInfraProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "NVARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(80)",
                oldMaxLength: 80);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "NVARCHAR(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)",
                oldMaxLength: 100);
        }
    }
}
