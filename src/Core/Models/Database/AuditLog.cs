using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrackeysBot.Models.Database
{
    [Table("audit_log")]
    public class AuditLog
    {
        [Key, Column("log_id"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int LogId { get; set; }
        [Column("date"), Required]
        public DateTime Date { get; set; }
        [Column("infractions_id")]
        public int InfractionsId { get; set; }
        [Column("moderation_types_type_id")]
        public int ModerationTypeId { get; set; }
        [Column("description"), Required]
        public string Description { get; set; }
        [Column("user_data__user_id")]
        public int UserId { get; set; }
    }
}