using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipesRemixed.Recipes.Data.Migrations
{
    public partial class RecipesInitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Ingredients = table.Column<string>(nullable: true),
                    Instructions = table.Column<string>(nullable: true),
                    TypeOfDish = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Calories = table.Column<int>(nullable: false),
                    Vegetarian = table.Column<bool>(nullable: false),
                    Vegan = table.Column<bool>(nullable: false),
                    Allergies = table.Column<string>(nullable: true),
                    ChefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipesRemix",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Ingredients = table.Column<string>(nullable: true),
                    Instructions = table.Column<string>(nullable: true),
                    TypeOfDish = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Calories = table.Column<int>(nullable: false),
                    Vegetarian = table.Column<bool>(nullable: false),
                    Vegan = table.Column<bool>(nullable: false),
                    Allergies = table.Column<string>(nullable: true),
                    ChefId = table.Column<int>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesRemix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipesRemix_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipesRemix_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ChefId",
                table: "Recipes",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipesRemix_ChefId",
                table: "RecipesRemix",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipesRemix_RecipeId",
                table: "RecipesRemix",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipesRemix");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Chefs");
        }
    }
}
