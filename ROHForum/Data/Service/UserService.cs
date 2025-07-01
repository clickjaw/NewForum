using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface IUserService
    {
        public UserModel GetUser(int id);
        public UserModel ValidateUser(UserModel userModel);
        public bool AddNewUser(UserModel newModel);
        public UserModel GetUserByUsername(string userName);
        public bool UpdateUserUpvotes(int id);
        public bool UpdateUserDownvotes(int id);

        List<UserModel> GetUsersByStartingLetters(string search);
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

        public List<UserModel> GetUsersByStartingLetters(string search)
        {
            return _dbContext.Users.Where(x => x.Username.StartsWith(search)).ToList();

        }

        public UserModel GetUserByUsername(string userName)
        {
            return _dbContext.Users.Where(x => x.Username == userName).FirstOrDefault();

        }

        public bool UpdateUserUpvotes(int id)
        {
            UserModel userModel = GetUser(id);
            userModel.TotalUpvotes++;
            _dbContext.SaveChanges();
            return true;
            
        }

        public bool UpdateUserDownvotes(int id)
        {
            UserModel userModel = GetUser(id);
            userModel.TotalDownvotes++;
            _dbContext.SaveChanges();
            return true;

        }

        public UserModel ValidateUser(UserModel userModel)
        {
            UserModel invalidPassword = new()
            {
                Password = "InvalidPassword"
            };
            UserModel invalidUser = new()
            {
                Password = "InvalidUser"
            };
            UserModel dbUser = GetUserByUsername(userModel.Username);
            if(dbUser is null)
            {
                return invalidUser;

            }
            else if(dbUser.Password == userModel.Password)
            {
                return dbUser;
;            }
            else
            {
                return invalidPassword;
            }

        }

        
    }
}
