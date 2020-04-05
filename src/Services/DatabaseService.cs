using System;
using BrackeysBot.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BrackeysBot.Services
{
    public class DatabaseService : BrackeysBotService, IInitializeableService
    {
        private readonly DataService _dataService;

        private BotConfiguration _config;
        private DatabaseContext _context;
        
        public DatabaseService(DataService dataService)
        {
            _dataService = dataService;
        }
        public void Initialize()
        {
            _config = _dataService.Configuration;
            _context = new DatabaseContext(_config);
        }
    }
}