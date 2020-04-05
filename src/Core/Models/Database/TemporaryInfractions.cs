using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrackeysBot.Models.Database
{
    [Table("temporary_infractions")]
    public class TemporaryInfractions
    {
        [Key, Column("temp_infr_id"), Required]
        public int TemporaryInfractionId { get; set; }
        [Column("infractions_id"), Required]
        public int InfractionId { get; set; }
        [Column("end_date"), Required]
        public DateTime EndDate { get; set; }
    }
}