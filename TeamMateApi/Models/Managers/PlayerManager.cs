using AutoMapper;
using MongoDB.Bson.IO;
using TeamMateApi.Models.DTOs;
using TeamMateServer.Data.Entities;
using System.Text.Json;
using TeamMateServer.Data.Repositories;

namespace TeamMateApi.Models.Managers
{
    public class PlayerManager
    {
        private readonly IRepository<PlayerEntity> playerRepository;
        private readonly IMapper mapper;

        public PlayerManager(IRepository<PlayerEntity> playerRepository, IMapper mapper)
        {
            this.playerRepository = playerRepository;
            this.mapper = mapper;
        }

        public async Task<List<PlayerDTO>> GetAllPlayersAsync()
        {
            var players = await playerRepository.GetAllAsync();
            return mapper.Map<List<PlayerDTO>>(players);
        }

        public async Task<PlayerDTO> GetPlayerByIdAsync(int id)
        {
            var player = await playerRepository.GetByIdAsync(id);
            return mapper.Map<PlayerDTO>(player);
        }

        public async Task CreatePlayerAsync(PlayerDTO playerDto)
        {
            var player = mapper.Map<PlayerEntity>(playerDto);
            var serializedPlayer = JsonSerializer.Serialize(player);
            System.Diagnostics.Debug.WriteLine(serializedPlayer);
            await playerRepository.CreateAsync(player);
        }

        public async Task UpdatePlayerAsync(int id, PlayerDTO playerDto)
        {
            var player = mapper.Map<PlayerEntity>(playerDto);
            await playerRepository.UpdateAsync(id, player);
        }

        public async Task DeletePlayerAsync(int id)
        {
            await playerRepository.DeleteAsync(id);
        }

        public async Task<List<PlayerDTO>> SearchPlayersAsync(string text)
        {
            var players = await playerRepository.SearchAsync(text);
            return mapper.Map<List<PlayerDTO>>(players);
        }
    }
}
