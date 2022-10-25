using pizzeriaApiRest.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzeriaApiRest.DTOs
{
    public class PizzaResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string Vegan { get; set; }
        public string Spicy { get; set; }
        public List<PizzaIngrediantResponceDTO> PizzaIngrediants { get; set; }

        public PizzaResponseDTO()
        {
            PizzaIngrediants = new List<PizzaIngrediantResponceDTO>();
        }
    }
}
