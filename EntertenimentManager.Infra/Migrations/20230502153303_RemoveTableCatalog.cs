using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntertenimentManagement.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Catalog",
                table: "Category");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropColumn(
                name: "CatalogId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "CatalogId",
                table: "Category",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_CatalogId",
                table: "Category",
                newName: "IX_Category_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Owner",
                table: "Category",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Owner",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Category",
                newName: "CatalogId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_OwnerId",
                table: "Category",
                newName: "IX_Category_CatalogId");

            migrationBuilder.AddColumn<int>(
                name: "CatalogId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Catalog",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_UserId",
                table: "Catalog",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Catalog",
                table: "Category",
                column: "CatalogId",
                principalTable: "Catalog",
                principalColumn: "Id");
        }
    }
}
