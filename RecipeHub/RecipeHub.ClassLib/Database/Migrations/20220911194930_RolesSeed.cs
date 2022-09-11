using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeHub.ClassLib.Migrations
{
    public partial class RolesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO public." + "\"Role\"" +
                                 "(\"Id\", \"Name\") VALUES " +
                                 "('815b90b9-7098-4cc6-b8bc-d25f373d418e', 'Admin')," +
                                 "('cb120046-0ef7-4ddb-b04a-05ff2c2f49b6', 'Regular')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM public.\"Role\"");
        }
    }
}
