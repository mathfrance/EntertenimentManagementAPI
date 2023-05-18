using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntertenimentManagement.Infra.Migrations
{
    /// <inheritdoc />
    public partial class PersonalListWithGenericsItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPersonalList");

            migrationBuilder.AddColumn<int>(
                name: "BelongsToId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_BelongsToId",
                table: "Item",
                column: "BelongsToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_PersonalList_BelongsToId",
                table: "Item",
                column: "BelongsToId",
                principalTable: "PersonalList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_PersonalList_BelongsToId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_BelongsToId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "BelongsToId",
                table: "Item");

            migrationBuilder.CreateTable(
                name: "ItemPersonalList",
                columns: table => new
                {
                    BelongsToId = table.Column<int>(type: "int", nullable: false),
                    ItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPersonalList", x => new { x.BelongsToId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_ItemPersonalList_Item_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPersonalList_PersonalList_BelongsToId",
                        column: x => x.BelongsToId,
                        principalTable: "PersonalList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPersonalList_ItemsId",
                table: "ItemPersonalList",
                column: "ItemsId");
        }
    }
}
