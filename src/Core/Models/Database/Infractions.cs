using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrackeysBot.Models.Database
{
    [Table("infactions")]
    public class Infractions
    {
        [Key, Column("id"), Required]
        public int Id { get; set; }
        [Column("date"), Required]
        public DateTime Date { get; set; }
        [Column("reason"), Required]
        public string Reason { get; set; }
        [Column("additional_info")]
        public string AdditionalInfo { get; set; }
        [Column("moderation_types_type_id"), Required]
        public int ModerationTypeId { get; set; }
        [Column("target_user_id"), Required]
        public int TargetUserId { get; set; }
        [Column("moderator_user_id"), Required]
        public int ModeratorUserId { get; set; }
    }
}