using System.ComponentModel.DataAnnotations;

namespace ROHForum.Data
{
    public class TagModel
    {
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
