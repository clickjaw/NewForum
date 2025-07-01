using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface IPostService
    {
        bool AddPost(PostsModel postModel);
        bool UpdatePostVotes(PostsModel postModel);
        PostsModel GetVoteDifference(PostsModel postsModel);
        List<PostsModel> GetTopPosts();
        List<PostsModel> GetPostsByUser(int userID);
        PostsModel GetSinglePost(int id);
        List<PostsModel> GetPostsByStartingLetters(string search);
        List<PostsModel> GetNewPosts();
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
                postModel.PostCreated = DateTime.Now;
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

        public List<PostsModel> GetNewPosts()
        {
            return _dbContext.Posts.OrderByDescending(x => x.PostCreated).ToList();
        }

        public List<PostsModel> GetPostsByStartingLetters(string search)
        {
            return _dbContext.Posts.Where(x=>x.Title.StartsWith(search)).ToList();
            //return _dbContext.Posts.OrderByDescending(x => x.Title.StartsWith(search)).ToList();
        }

        public PostsModel GetVoteDifference(PostsModel postModel)
        {
            postModel.VoteDifference = postModel.Upvote - postModel.Downvote;

            return postModel;
        }

        public PostsModel GetSinglePost(int id)
        {

            return _postData.GetByID(id);
            
        }



        public List<PostsModel> GetPostsByUser(int userID)
        {
            return _dbContext.Posts.Where(x => x.UserId == userID).ToList();
        }

    }
}
