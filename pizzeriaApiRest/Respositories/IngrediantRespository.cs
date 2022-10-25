using pizzeriaApiRest.Models;
using pizzeriaApiRest.Tools;
using System.Linq;

namespace pizzeriaApiRest.Respositories
{
    public class IngrediantRespository : BaseRespository<Ingrediants>
    {
        public IngrediantRespository(DataDbContext dataContext) : base(dataContext)
        {
        }

        public override bool Delete(Ingrediants element)
        {
            _dataContext.Ingrediants.Remove(element);
            return _dataContext.SaveChanges() > 0;
        }

        public override List<Ingrediants> FindAll()
        {
            return _dataContext.Ingrediants.ToList();
        }

        public override Ingrediants FindById(int id)
        {
            return _dataContext.Ingrediants.FirstOrDefault(i => i.Id == id);
        }

        public override bool Save(Ingrediants element)
        {
            _dataContext.Ingrediants.Add(element);
            return _dataContext.SaveChanges() > 0;
        }

        public override List<Ingrediants> SearchAll(Func<Ingrediants, bool> SearchMethod)
        {
            return _dataContext.Ingrediants.Where(SearchMethod).ToList();
        }

        public override Ingrediants SearchOne(Func<Ingrediants, bool> SearchMethod)
        {
            return _dataContext.Ingrediants.FirstOrDefault(SearchMethod);
        }
    }
}
