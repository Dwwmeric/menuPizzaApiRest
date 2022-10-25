using System.ComponentModel.DataAnnotations.Schema;

namespace pizzeriaApiRest.DTOs
{
    public class IngrediantResponseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }
    }
}
