using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSite.Models
{
    [Table("WebSiteHit")]
    public class WebSiteHit
    {
        [Key]
        public int WebSiteHitId { get; set; }
        public int HitCount { get; set; }
    }
}