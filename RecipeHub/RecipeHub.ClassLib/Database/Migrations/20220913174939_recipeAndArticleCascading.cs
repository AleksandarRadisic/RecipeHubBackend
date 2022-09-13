using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.ClassLib.Migrations
{
    public partial class recipeAndArticleCascading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Articles_ArticleId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Recipes_RecipeId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Recipes_RecipeId",
                table: "RecipeIngredient");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Articles_ArticleId",
                table: "Picture",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Recipes_RecipeId",
                table: "Picture",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Recipes_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Articles_ArticleId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Recipes_RecipeId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Recipes_RecipeId",
                table: "RecipeIngredient");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Articles_ArticleId",
                table: "Picture",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Recipes_RecipeId",
                table: "Picture",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Recipes_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
