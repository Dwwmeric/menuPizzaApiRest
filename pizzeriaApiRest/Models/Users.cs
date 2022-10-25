using System.ComponentModel.DataAnnotations.Schema;

namespace pizzeriaApiRest.Models
{
    [Table("users")]
    public class Users
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("adresse")]
        public string Adresse { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("password")]
        public string Password { get; set; }

    }
}
