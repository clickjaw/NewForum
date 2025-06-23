using System.ComponentModel.DataAnnotations;

namespace ROHForum.Data.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? UserPic { get; set; }
        public string? Tags { get; set; }
        public int TotalUpvotes { get; set; }
        public int TotalDownvotes { get; set; }

        public string? Password { get; set; }

        public List<CommentsModel> UserComments { get; set; }
        public List<PostsModel> UserPosts { get; set; }
    }
}
