using Microsoft.AspNetCore.Mvc;
using TeamMateApi.Models.DTOs;
using TeamMateApi.Models.Managers;

namespace TeamMateApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamManager _teamManager;

        public TeamController(TeamManager teamManager)
        {
            _teamManager = teamManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeamDTO>>> GetAllTeams()
        {
            var teams = await _teamManager.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeamById(int id)
        {
            var team = await _teamManager.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromBody] TeamDTO teamDto)
        {
            await _teamManager.CreateTeamAsync(teamDto);
            return CreatedAtAction(nameof(GetTeamById), new { id = teamDto.Id }, teamDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeam(int id, [FromBody] TeamDTO teamDto)
        {
            await _teamManager.UpdateTeamAsync(id, teamDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            await _teamManager.DeleteTeamAsync(id);
            return NoContent();
        }
    }
}
