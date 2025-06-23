using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface IPostService
    {
        bool AddPost(PostsModel postModel);
        bool UpdatePostVotes(PostsModel postModel);
        PostsModel GetVoteDifference(PostsModel postsModel);
        List<PostsModel> GetTopPosts();
    }

    public class PostService : IPostService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IPostData _postData;
        public PostService(DatabaseContext dbContext, IPostData postData)
        {
            _dbContext = dbContext;
            _postData = postData;
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

        public bool UpdatePostVotes(PostsModel postModel)
        {
            try
            {
                _postData.UpdatePostVotes(postModel);

                GetVoteDifference(postModel);

                _dbContext.SaveChanges();



                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<PostsModel> GetTopPosts()
        {
            return _dbContext.Posts.OrderByDescending(x => x.VoteDifference).ToList();
        }

        public PostsModel GetVoteDifference(PostsModel postModel)
        {
            postModel.VoteDifference = postModel.Upvote - postModel.Downvote;

            return postModel;
        }

    }
}
