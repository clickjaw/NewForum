using System.ComponentModel.DataAnnotations;

namespace ROHForum.Data.Models
{
    public class CommentsModel
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string? Comment { get; set; }
        public int UserId { get; set; }
        public DateTime CommentCreated { get; set; } = DateTime.Now;
        public DateTime CommentUpdated { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public string? Username { get; set; }

        //public UserModel UserModel { get; set; }

    }
}
