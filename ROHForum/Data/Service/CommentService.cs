using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface ICommentService
    {
        bool AddComment(CommentsModel commentModel);
        bool UpdatePostVotes(PostsModel postModel);
        PostsModel GetVoteDifference(PostsModel postsModel);
        List<CommentsModel> GetAllComments(int postId);

        PostsModel GetSinglePost(int id);
    }

    public class CommentService : ICommentService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IPostData _postData;
        public CommentService(DatabaseContext dbContext, IPostData postData)
        {
            _dbContext = dbContext;
            _postData = postData;
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

        public List<CommentsModel> GetAllComments(int postId)
        {
            return _dbContext.Comments.OrderByDescending(x => x.Upvote).Where(x=>x.PostId == postId).ToList();
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

    }
}
