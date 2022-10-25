using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizzeriaApiRest.Models
{
    [Table("pizza_ingrediant")]
    public class PizzasIngrediants
    {
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("pizza_id")]
        [JsonIgnore]
        public Pizza Pizza { get; set; }

        [ForeignKey("ingrediant_id")]
        [JsonIgnore]
        public Ingrediants Ingrediants { get; set; }
    }
}
