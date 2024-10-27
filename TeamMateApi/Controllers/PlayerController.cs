using Microsoft.AspNetCore.Mvc;
using TeamMateApi.Models.DTOs;
using TeamMateApi.Models.Managers;

namespace TeamMateApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerManager playerManager;

        public PlayerController(PlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlayerDTO>>> GetAllPlayers()
        {
            var players = await playerManager.GetAllPlayersAsync();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayerById(int id)
        {
            var player = await playerManager.GetPlayerByIdAsync(id);
            if (player == null) return NotFound();
            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlayer([FromBody] PlayerDTO playerDto)
        {
            await playerManager.CreatePlayerAsync(playerDto);
            return CreatedAtAction(nameof(GetPlayerById), new { id = playerDto.Id }, playerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlayer(int id, [FromBody] PlayerDTO playerDto)
        {
            await playerManager.UpdatePlayerAsync(id, playerDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            await playerManager.DeletePlayerAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<PlayerDTO>>> SearchPlayers([FromQuery] string query)
        {
            var players = await playerManager.SearchPlayersAsync(query);
            return Ok(players);
        }
    }
}
