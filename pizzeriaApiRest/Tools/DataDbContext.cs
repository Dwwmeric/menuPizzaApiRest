using Microsoft.EntityFrameworkCore;
using pizzeriaApiRest.Models;

namespace pizzeriaApiRest.Tools
{
    public class DataDbContext : DbContext
    {
        public DbSet<Ingrediants> Ingrediants { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<PizzasIngrediants> PizzasIngrediants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localDb)\pizza_api_rest;Integrated Security=True");
        }
    }
}
