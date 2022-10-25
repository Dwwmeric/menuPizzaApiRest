using System.ComponentModel.DataAnnotations.Schema;

namespace pizzeriaApiRest.Models
{
    [Table("pizzas")]
    public class Pizza
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price")]
        public string Price { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("statut_vegan")]
        public string Vegan { get; set; }

        [Column("statut_spicy")]
        public string Spicy { get; set; }

        public List<PizzasIngrediants> PizzaIngrediants { get; set; }
        public Pizza()
        {
            PizzaIngrediants = new List<PizzasIngrediants>();
        }
    }
}
