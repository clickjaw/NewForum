using System.ComponentModel.DataAnnotations;

namespace ROHForum.Data.Models
{
    public class ModModel
    {
        [Key]
        public int ModId { get; set; }
        public int UserId { get; set; }
        public string? ModTags { get; set; }
    }
}
