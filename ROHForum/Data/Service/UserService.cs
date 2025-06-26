using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface IUserService
    {
        public UserModel GetUser(int id);
        public UserModel ValidateUser(UserModel userModel);
        public bool AddNewUser(UserModel newModel);
        public UserModel GetUserByUsername(string userName);
    }

    public class UserService : IUserService
    {
        private readonly DatabaseContext _dbContext;

        public UserService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddNewUser(UserModel newUser)
        {
            if (newUser.Username != String.Empty && newUser.Password != String.Empty)
            {

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        public UserModel GetUser(int id) {

            return _dbContext.Users.Where(x=>x.UserId == id).FirstOrDefault();
        }

        public UserModel GetUserByUsername(string userName)
        {
            return _dbContext.Users.Where(x => x.Username == userName).FirstOrDefault();

        }

        public UserModel ValidateUser(UserModel userModel)
        {
            UserModel invalidUser = new();
            UserModel dbUser = GetUserByUsername(userModel.Username);
            if(dbUser.Password == userModel.Password)
            {
                return dbUser;
;            }
            else
            {
                return invalidUser;
            }

        }

        
    }
}
