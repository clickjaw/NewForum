using Microsoft.EntityFrameworkCore;
using ROHForum.Data.Models;

namespace ROHForum.Data
{
    public interface ICommentData : IGenericRepository<CommentsModel>
    {
        public bool UpdateCommentUpvotes(CommentsModel commentModel);
        public bool UpdateCommentDownvotes(CommentsModel commentModel);


    }

    public class CommentData : GenericRepository<CommentsModel>, ICommentData
    {
        public CommentData(DatabaseContext context) : base(context) { }

        public bool UpdateCommentUpvotes(CommentsModel commentModel)
        {
            try
            {
                _dbSet.Where(x => x.CommentId == commentModel.CommentId)
                        .ExecuteUpdate(a => a.SetProperty(b => b.Upvote, commentModel.Upvote));




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
                _dbSet.Where(x => x.CommentId == commentModel.CommentId)
                        .ExecuteUpdate(a => a.SetProperty(b => b.Downvote, commentModel.Downvote));




                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
