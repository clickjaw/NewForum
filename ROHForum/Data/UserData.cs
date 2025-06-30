using Microsoft.EntityFrameworkCore;
using ROHForum.Data.Models;

namespace ROHForum.Data
{
    public interface IUserData : IGenericRepository<UserModel>
    {
    
        public bool UpdateUserUpvotes(UserModel userModel);

    }

    public class UserData : GenericRepository<UserModel>, IUserData
    {
        public UserData(DatabaseContext context) : base(context) { }

        public bool UpdateUserUpvotes(UserModel userModel)
        {
            try
            {
                
                _dbSet.Where(x => x.UserId == userModel.UserId)
                        .ExecuteUpdate(a => a.SetProperty(b => b.TotalUpvotes, userModel.TotalUpvotes));




                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
