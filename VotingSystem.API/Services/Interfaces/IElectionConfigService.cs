using VotingSystem.API.DTOs;

namespace VotingSystem.API.Services.Interfaces
{
    public interface IElectionConfigService
    {
        ElectionConfigDto GetElectionConfig();
        void SetElectionConfig(DateTime startTime, DateTime endTime);
    }
}
