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
                name: "PersonalListGame");

            migrationBuilder.DropTable(
                name: "PersonalListMovie");

            migrationBuilder.AddColumn<int>(
                name: "BelongsToId",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BelongsToId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_BelongsToId",
                table: "Movie",
                column: "BelongsToId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_BelongsToId",
                table: "Game",
                column: "BelongsToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_PersonalList",
                table: "Game",
                column: "BelongsToId",
                principalTable: "PersonalList",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_PersonalList_BelongsToId",
                table: "Movie",
                column: "BelongsToId",
                principalTable: "PersonalList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_PersonalList",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_PersonalList_BelongsToId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_BelongsToId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Game_BelongsToId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "BelongsToId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "BelongsToId",
                table: "Game");

            migrationBuilder.CreateTable(
                name: "PersonalListGame",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PersonalListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalListGame", x => new { x.GameId, x.PersonalListId });
                    table.ForeignKey(
                        name: "FK_PersonalListGame_GameId",
                        column: x => x.GameId,
                        principalTable: "PersonalList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalListGame_PersonalListId",
                        column: x => x.PersonalListId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalListMovie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    PersonalListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalListMovie", x => new { x.MovieId, x.PersonalListId });
                    table.ForeignKey(
                        name: "FK_PersonalListMovie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "PersonalList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalListMovie_PersonalListId",
                        column: x => x.PersonalListId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalListGame_PersonalListId",
                table: "PersonalListGame",
                column: "PersonalListId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalListMovie_PersonalListId",
                table: "PersonalListMovie",
                column: "PersonalListId");
        }
    }
}
