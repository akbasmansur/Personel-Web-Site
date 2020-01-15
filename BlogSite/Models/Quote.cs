using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSite.Models
{
    [Table("Quote")]
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }
        public string TheQuote { get; set; }
        public string QuoteOwner { get; set; }
    }
}