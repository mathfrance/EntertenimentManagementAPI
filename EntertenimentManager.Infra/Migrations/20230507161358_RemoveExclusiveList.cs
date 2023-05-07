using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntertenimentManagement.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExclusiveList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exclusive",
                table: "PersonalList");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Exclusive",
                table: "PersonalList",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }
    }
}
