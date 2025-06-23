using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface IAddPostService
    {
        bool AddPost(PostsModel postModel);
        List<PostsModel> GetTopPosts();
    }

    public class AddPostService : IAddPostService
    {
        private readonly DatabaseContext _dbContext;

        public AddPostService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddPost(PostsModel postModel)
        {
            try
            {
                _dbContext.Posts.Add(postModel);


                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<PostsModel> GetTopPosts()
        {
            return _dbContext.Posts.OrderByDescending(x => x.Upvote).ToList();
        }

    }
}
