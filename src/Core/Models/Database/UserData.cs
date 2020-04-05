using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrackeysBot.Models.Database
{
    [Table("user_data")]
    public class UserData
    {
        [Key, Column("user_id"), Required]
        public int UserID { get; set;}
        [Key, Column("start"), Required]
        public int Stars { get; set; }
    }
}