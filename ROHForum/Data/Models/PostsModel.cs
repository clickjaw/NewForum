using System.ComponentModel.DataAnnotations;

namespace ROHForum.Data.Models
{
    public class PostsModel
    {
        [Key]
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Tags { get; set; }
        public int UserId { get; set; }
        public DateTime PostCreated { get; set; } = DateTime.Now;
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public int VoteDifference { get; set; }
        
        public UserModel UserModel { get; set; }
        public List<CommentsModel> PostComments { get; set; }
    }
}
