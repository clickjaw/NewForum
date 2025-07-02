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
        private readonly IUserData _userData;
        private readonly ICommentData _commentData;
        public PostService(DatabaseContext dbContext, IPostData postData, IUserData userData, ICommentData commentData)
        {
            _dbContext = dbContext;
            _postData = postData;
            _userData = userData;
            _commentData = commentData;
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
            List<PostsModel> returnPosts = _dbContext.Posts.OrderByDescending(x => x.VoteDifference).ToList();
            foreach (var post in returnPosts) {
                UserModel userModel = _userData.GetUserById(post.UserId);
                List<CommentsModel> comments = _commentData.GetAllComments(post.PostId);
                post.PostComments = comments;
                post.UserModel = userModel;

            }

            return returnPosts;
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
            PostsModel postModel = _postData.GetByID(id);
            UserModel userModel = _userData.GetUserById(postModel.UserId);
            postModel.UserModel = userModel;
            return postModel;
            
        }



        public List<PostsModel> GetPostsByUser(int userID)
        {
            return _dbContext.Posts.Where(x => x.UserId == userID).ToList();
        }

    }
}
