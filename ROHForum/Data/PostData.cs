using Microsoft.EntityFrameworkCore;
using ROHForum.Data.Models;

namespace ROHForum.Data
{
    public interface IPostData: IGenericRepository<PostsModel>
    {
        public bool UpdatePostVotes(PostsModel postModel);

    }

    public class PostData: GenericRepository<PostsModel>, IPostData
    {
        public PostData(DatabaseContext context) : base(context) { }

        public bool UpdatePostVotes(PostsModel postModel)
        {
            try
            {
                _dbSet.Where(x => x.PostId == postModel.PostId)
                        .ExecuteUpdate(a => a.SetProperty(b => b.Upvote, postModel.Upvote));



                //_dbContext.Posts.Update(postModel);
                //_dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
