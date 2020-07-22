using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipesRemixed.Recipes.Data.Migrations
{
    public partial class RecipeRemixCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipesRemix_Recipes_RecipeId",
                table: "RecipesRemix");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipesRemix",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesRemix_Recipes_RecipeId",
                table: "RecipesRemix",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipesRemix_Recipes_RecipeId",
                table: "RecipesRemix");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipesRemix",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipesRemix_Recipes_RecipeId",
                table: "RecipesRemix",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
