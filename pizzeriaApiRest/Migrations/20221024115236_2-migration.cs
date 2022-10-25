using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pizzeriaApiRest.Migrations
{
    public partial class _2migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingrediants_pizzas_PizzaId",
                table: "ingrediants");

            migrationBuilder.DropIndex(
                name: "IX_ingrediants_PizzaId",
                table: "ingrediants");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "ingrediants");

            migrationBuilder.CreateTable(
                name: "pizza_ingrediant",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pizza_id = table.Column<int>(type: "int", nullable: false),
                    ingrediant_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pizza_ingrediant", x => x.id);
                    table.ForeignKey(
                        name: "FK_pizza_ingrediant_ingrediants_ingrediant_id",
                        column: x => x.ingrediant_id,
                        principalTable: "ingrediants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pizza_ingrediant_pizzas_pizza_id",
                        column: x => x.pizza_id,
                        principalTable: "pizzas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pizza_ingrediant_ingrediant_id",
                table: "pizza_ingrediant",
                column: "ingrediant_id");

            migrationBuilder.CreateIndex(
                name: "IX_pizza_ingrediant_pizza_id",
                table: "pizza_ingrediant",
                column: "pizza_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pizza_ingrediant");

            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "ingrediants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ingrediants_PizzaId",
                table: "ingrediants",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ingrediants_pizzas_PizzaId",
                table: "ingrediants",
                column: "PizzaId",
                principalTable: "pizzas",
                principalColumn: "id");
        }
    }
}
