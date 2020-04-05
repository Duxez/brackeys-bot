using System;
using Microsoft.EntityFrameworkCore;

namespace BrackeysBot.Models.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly BotConfiguration _config;

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Infractions> Infractions { get; set; }
        public DbSet<ModerationTypes> ModerationTypes { get; set; }
        public DbSet<TemporaryInfractions> TemporaryInfractions { get; set; }
        public DbSet<UserData> UserData { get; set; }

        public DatabaseContext(BotConfiguration config)
        {
            Console.WriteLine("Creating");
            _config = config;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("Configuring");
            Console.WriteLine(_config.Database);
            optionsBuilder.UseMySQL(
                $"server={_config.Server};database={_config.Database};user={_config.User};password=${_config.Password}");
        }
    }
}