using pizzeriaApiRest.Models;

namespace pizzeriaApiRest.DTOs
{
    public class PizzaRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string Vegan { get; set; }
        public string Spicy { get; set; }
    }
}
