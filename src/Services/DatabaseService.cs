using System;
using System.Linq;
using System.Threading.Tasks;
using BrackeysBot.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BrackeysBot.Services
{
    public class DatabaseService : BrackeysBotService, IInitializeableService
    {
        private readonly DataService _dataService;

        private BotConfiguration _config;
        public DatabaseContext Context { get; private set; }

        public DatabaseService(DataService dataService)
        {
            _dataService = dataService;
        }
        public void Initialize()
        {
            _config = _dataService.Configuration;
            Context = new DatabaseContext(_config);
        }

        public async Task<Models.Database.UserData> GetOrCreate(ulong id) => await Context.UserData.FirstOrDefaultAsync(u => u.UserId == id) ?? await CreateAndSaveUser(id);

        private async Task<Models.Database.UserData> CreateAndSaveUser(ulong id)
        {
            var userData = new Models.Database.UserData {UserId = id, Stars = 0};
            await Context.UserData.AddAsync(userData);
            await Context.SaveChangesAsync();
            return userData;
        }
        
        
    }
}