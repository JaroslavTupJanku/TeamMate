using Microsoft.AspNetCore.Mvc;
using TeamMateApi.Models.DTOs;
using TeamMateApi.Models.Managers;

namespace TeamMateApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamManager teamManager;

        public TeamController(TeamManager teamManager)
        {
            this.teamManager = teamManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeamDTO>>> GetAllTeams()
        {
            var teams = await teamManager.GetAllTeamsAsync();
            return Ok(teams); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeamById(int id)
        {
            var team = await teamManager.GetTeamByIdAsync(id);
            return team == null ? NotFound() : Ok(team);    
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<TeamDTO>>> SearchTeams([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            var teams = await teamManager.SearchTeamsAsync(query);

            if (teams == null || teams.Count == 0)
            {
                return NotFound("No teams match the search query.");
            }

            return Ok(teams);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromBody] TeamDTO teamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            await teamManager.CreateTeamAsync(teamDto);
            return CreatedAtAction(nameof(GetTeamById), new { id = teamDto.Id }, teamDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeam(int id, [FromBody] TeamDTO teamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await teamManager.GetTeamByIdAsync(id) == null)
            {
                return NotFound();
            }

            await teamManager.UpdateTeamAsync(id, teamDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            if (await teamManager.GetTeamByIdAsync(id) == null)
            {
                return NotFound();
            }

            await teamManager.DeleteTeamAsync(id);
            return NoContent();
        }
    }
}
