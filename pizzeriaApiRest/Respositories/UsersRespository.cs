using pizzeriaApiRest.Models;
using pizzeriaApiRest.Tools;

namespace pizzeriaApiRest.Respositories
{
    public class UsersRespository : BaseRespository<Users>
    {
        public UsersRespository(DataDbContext dataContext) : base(dataContext)
        {
        }

        public override bool Delete(Users element)
        {
            _dataContext.Users.Remove(element);
            return _dataContext.SaveChanges() > 0;
        }

        public override List<Users> FindAll()
        {
            return _dataContext.Users.ToList();
        }

        public override Users FindById(int id)
        {
            return _dataContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public override bool Save(Users element)
        {
            _dataContext.Users.Add(element);
            return _dataContext.SaveChanges() > 0;
        }

        public override List<Users> SearchAll(Func<Users, bool> SearchMethod)
        {
            return _dataContext.Users.Where(SearchMethod).ToList();
        }

        public override Users SearchOne(Func<Users, bool> SearchMethod)
        {
            return _dataContext.Users.FirstOrDefault(SearchMethod);
        }
    }
}
