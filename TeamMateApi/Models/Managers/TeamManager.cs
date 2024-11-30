using AutoMapper;
using TeamMateApi.Models.DTOs;
using TeamMateServer.Data.Entities;
using TeamMateServer.Data.Repositories;

namespace TeamMateApi.Models.Managers
{
    public class TeamManager
    {
        private readonly IRepository<TeamEntity> _teamRepository;
        private readonly IMapper _mapper;

        public TeamManager(IRepository<TeamEntity> teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<List<TeamDTO>> SearchTeamsAsync(string query)
        {
            var teams = await _teamRepository.SearchAsync(query);
            return _mapper.Map<List<TeamDTO>>(teams);
        }

        public async Task<List<TeamDTO>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return _mapper.Map<List<TeamDTO>>(teams);
        }

        public async Task<TeamDTO> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task CreateTeamAsync(TeamDTO teamDto)
        {
            var team = _mapper.Map<TeamEntity>(teamDto);
            await _teamRepository.CreateAsync(team);
        }

        public async Task UpdateTeamAsync(int id, TeamDTO teamDto)
        {
            var team = _mapper.Map<TeamEntity>(teamDto);
            await _teamRepository.UpdateAsync(id, team);
        }

        public async Task DeleteTeamAsync(int id)
        {
            await _teamRepository.DeleteAsync(id);
        }
    }
}
