using System.ComponentModel.DataAnnotations.Schema;

namespace pizzeriaApiRest.DTOs
{
    public class UsersResponseDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Adresse { get; set; }

        public string Role { get; set; }

        public int Id { get; set; }
  
    }
}
