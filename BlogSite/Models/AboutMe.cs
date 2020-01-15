using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSite.Models
{
    [Table("AboutMe")]
    public class AboutMe
    {
        [Key]
        public int AboutMeId { get; set; }
        public string Title { get; set; }
        [Required]
        public string Explanation { get; set; }
    }
}