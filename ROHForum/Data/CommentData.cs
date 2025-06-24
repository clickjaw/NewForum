using Microsoft.EntityFrameworkCore;
using ROHForum.Data.Models;

namespace ROHForum.Data
{
    public interface ICommentData : IGenericRepository<PostsModel>
    {
        public bool UpdatePostVotes(PostsModel postModel);

    }

    /*public class CommentData : GenericRepository<CommentsModel>, ICommentData
    {
        public CommentData(DatabaseContext context) : base(context) { }

        public bool AddCommentToPost(CommentsModel commentModel)
        {
            try
            {
                _dbSet.Where(x => x.CommentId == commentModel.CommentId)
                        .ExecuteUpdate(a => a.SetProperty(b => b.Co, postModel.Upvote));



                
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }*/
}
