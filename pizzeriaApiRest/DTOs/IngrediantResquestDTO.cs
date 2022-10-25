using System.ComponentModel.DataAnnotations.Schema;

namespace pizzeriaApiRest.DTOs
{
    public class IngrediantResquestDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
