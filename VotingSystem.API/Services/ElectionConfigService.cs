using VotingSystem.API.Data;
using VotingSystem.API.DTOs;
using VotingSystem.API.Models;
using VotingSystem.API.Services.Interfaces;

namespace VotingSystem.API.Services
{
    public class ElectionConfigService : IElectionConfigService
    {
        private readonly VotingSystemDbContext _context;
        public ElectionConfigService(VotingSystemDbContext context)
        {
            _context = context;
        }
        public ElectionConfigDto GetElectionConfig()
        {
            var config = _context.ElectionConfig.FirstOrDefault();
            if (config == null)
                throw new InvalidOperationException("Election configuration not set.");

            return new ElectionConfigDto
            {
                StartTime = config.StartTime,
                EndTime = config.EndTime
            };
        }

        public void SetElectionConfig(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
                throw new InvalidOperationException("Start time must be before end time.");

            var existingConfig = _context.ElectionConfig.FirstOrDefault();

            if (existingConfig == null)
            {
                existingConfig = new ElectionConfig
                {
                    StartTime = startTime,
                    EndTime = endTime
                };
                _context.ElectionConfig.Add(existingConfig);
            }
            else
            {
                existingConfig.StartTime = startTime;
                existingConfig.EndTime = endTime;
                _context.ElectionConfig.Update(existingConfig);
            }

            _context.SaveChanges();
        }
    }
}
