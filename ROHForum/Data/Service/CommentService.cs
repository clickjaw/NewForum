using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface ICommentService
    {
        bool AddComment(CommentsModel commentModel);
        bool UpdateCommentUpvotes(CommentsModel commentModel);
        bool UpdateCommentDownvotes(CommentsModel commentModel);
        PostsModel GetVoteDifference(PostsModel postsModel);
        List<CommentsModel> GetAllComments(int postId);

        List<CommentsModel> GetCommentsByUser(int userId);
        PostsModel GetSinglePost(int id);
    }

    public class CommentService : ICommentService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IPostData _postData;
        private readonly ICommentData _commentData;
        private readonly IUserService _userService;
        public CommentService(DatabaseContext dbContext, IPostData postData, ICommentData commentData, IUserService userService)
        {
            _dbContext = dbContext;
            _postData = postData;
            _commentData = commentData;
            _userService = userService;
        }

        public bool AddComment(CommentsModel commentModel)
        {
            try {

                commentModel.Upvote = 0;
                commentModel.Downvote = 0;
                _dbContext.Comments.Add(commentModel);


                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCommentUpvotes(CommentsModel commentModel)
        {
            try
            {
                _commentData.UpdateCommentUpvotes(commentModel);
                
                _userService.UpdateUserUpvotes(commentModel.UserId);

                // GetVoteDifference(commentModel);

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool UpdateCommentDownvotes(CommentsModel commentModel)
        {
            try
            {
                _commentData.UpdateCommentDownvotes(commentModel);

                _userService.UpdateUserDownvotes(commentModel.UserId);

                // GetVoteDifference(commentModel);

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }



        public List<CommentsModel> GetAllComments(int postId)
        {
            return _dbContext.Comments.OrderByDescending(x => x.Upvote).Where(x=>x.PostId == postId).ToList();
        }

        public List<CommentsModel> GetCommentsByUser(int userId)
        {
            return _dbContext.Comments.OrderByDescending(x => x.CommentCreated).Where(x => x.UserId == userId).ToList();

        }


        public PostsModel GetVoteDifference(PostsModel postModel)
        {
            postModel.VoteDifference = postModel.Upvote - postModel.Downvote;

            return postModel;
        }

        /*public PostsModel GetVoteDifferenceComments(CommentsModel commentModel)
        {
            postModel.VoteDifference = postModel.Upvote - postModel.Downvote;

            return postModel;
        }*/

        public PostsModel GetSinglePost(int id)
        {

            return _postData.GetByID(id);


        }

    }
}
