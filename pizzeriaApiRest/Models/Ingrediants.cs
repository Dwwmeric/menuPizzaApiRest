using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizzeriaApiRest.Models
{
    [Table("ingrediants")]
    public class Ingrediants
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public List<PizzasIngrediants> _PizzaIngrediants { get; set; }
        public Ingrediants()
        {
            _PizzaIngrediants = new List<PizzasIngrediants>();
        }
    }
}
