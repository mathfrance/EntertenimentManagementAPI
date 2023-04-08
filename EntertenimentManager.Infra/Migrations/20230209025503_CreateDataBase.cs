using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntertenimentManagement.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Developer = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: true),
                    Title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Genre = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distributor = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: true),
                    Director = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: true),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Genre = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatforms",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlatformsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatforms", x => new { x.GameId, x.PlatformsId });
                    table.ForeignKey(
                        name: "FK_GamePlatforms_GameId",
                        column: x => x.PlatformsId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatforms_PlatformId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoles_RoleId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UserId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: true),
                    CatalogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Catalog",
                        column: x => x.CatalogId,
                        principalTable: "Catalog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonalList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Exclusive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalList_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

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
                name: "IX_Catalog_UserId",
                table: "Catalog",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CatalogId",
                table: "Category",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Title",
                table: "Game",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformsId",
                table: "GamePlatforms",
                column: "PlatformsId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Title",
                table: "Movie",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalList_CategoryId",
                table: "PersonalList",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalListGame_PersonalListId",
                table: "PersonalListGame",
                column: "PersonalListId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalListMovie_PersonalListId",
                table: "PersonalListMovie",
                column: "PersonalListId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlatforms");

            migrationBuilder.DropTable(
                name: "PersonalListGame");

            migrationBuilder.DropTable(
                name: "PersonalListMovie");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "PersonalList");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
