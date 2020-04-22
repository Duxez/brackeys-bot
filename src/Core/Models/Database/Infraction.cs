using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Humanizer;

namespace BrackeysBot.Models.Database
{
    [Table("infractions")]
    public class Infraction
    {
        [Key, Column("id"), Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("date"), Required]
        public DateTime DateTime { get; set; }
        [Column("reason"), Required]
        public string Reason { get; set; }
        [Column("additional_info")]
        public string AdditionalInfo { get; set; }
        [Column("moderation_types_type_id"), Required]
        public int ModerationTypeId { get; set; }
        [Column("target_user_id"), Required]
        public ulong TargetUserId { get; set; }
        [Column("moderator_user_id"), Required]
        public ulong ModeratorUserId { get; set; }
        [Column("end_date")]
        public DateTime EndDateTime { get; set; }

        public static Infraction Create(ulong targetUserId)
            => new Infraction
            {
                TargetUserId = targetUserId,
                DateTime = DateTime.UtcNow
            };

        public Infraction WithModerator(ulong moderatorId)
        {
            ModeratorUserId = moderatorId;
            return this;
        }
        
        public Infraction WithType(InfractionType type)
        {
            ModerationTypeId = (int)type;
            return this;
        }
        public Infraction WithDescription(string description)
        {
            Reason = description;
            return this;
        }
        public Infraction WithAdditionalInfo(string additionalInfo)
        {
            AdditionalInfo = additionalInfo;
            return this;
        }

        public Infraction WithEndDate(DateTime expire)
        {
            EndDateTime = expire;
            return this;
        }

        public override string ToString()
            => $"[{Id}] {Reason} • {((InfractionType) ModerationTypeId).Humanize()} • {DateTime.Humanize()}";
    }
}