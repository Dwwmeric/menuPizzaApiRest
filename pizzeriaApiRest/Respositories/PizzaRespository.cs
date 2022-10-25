using Microsoft.EntityFrameworkCore;
using pizzeriaApiRest.Models;
using pizzeriaApiRest.Tools;
using System.Linq;

namespace pizzeriaApiRest.Respositories
{
    public class PizzaRespository : BaseRespository<Pizza>
    {
        public PizzaRespository(DataDbContext dataContext) : base(dataContext)
        {
        }

        public override bool Delete(Pizza element)
        {
            _dataContext.Pizzas.Remove(element);
            return _dataContext.SaveChanges() > 0;
        }

        public override List<Pizza> FindAll()
        {
            return _dataContext.Pizzas.Include(p => p.PizzaIngrediants).ThenInclude(i => i.Ingrediants).ToList();
        }

        public override Pizza FindById(int id)
        {
            return _dataContext.Pizzas.Include(p => p.PizzaIngrediants).ThenInclude(m=> m.Ingrediants).FirstOrDefault(i => i.Id == id);
        }

        public override bool Save(Pizza element)
        {
            _dataContext.Pizzas.Add(element);
            return _dataContext.SaveChanges() > 0;
        }

        public override List<Pizza> SearchAll(Func<Pizza, bool> SearchMethod)
        {
            throw new NotImplementedException();
        }

        public override Pizza SearchOne(Func<Pizza, bool> SearchMethod)
        {
            return _dataContext.Pizzas.FirstOrDefault(SearchMethod);
        }
    }
}
