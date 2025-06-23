using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface IUserService
    {
        public UserModel GetUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly DatabaseContext _dbContext;

        public UserService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserModel GetUser(int id) {

            return _dbContext.Users.Where(x=>x.UserId == id).FirstOrDefault();
        }

        
    }
}
