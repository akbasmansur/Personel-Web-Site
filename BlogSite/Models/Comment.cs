using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSite.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string E_mail { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Confirmation { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}