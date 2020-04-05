using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrackeysBot.Models.Database
{
    [Table("moderation_types")]
    public class ModerationTypes
    {
        [Key, Column("type_id"), Required]
        public int TypeId { get; set; }
        [Column("type_desc"), Required]
        public string TypeDescription { get; set; }
    }
}