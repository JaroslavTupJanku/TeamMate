using MongoDB.Driver;
using TeamMateServer.Data.Entities;

namespace TeamMateApi.Data
{
    public class MongoDBInitializer
    {
        private readonly IMongoCollection<PlayerEntity> playersCollection;
        private readonly IMongoCollection<TeamEntity> teamsCollection;

        public MongoDBInitializer(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            playersCollection = database.GetCollection<PlayerEntity>("Players");
            teamsCollection = database.GetCollection<TeamEntity>("Teams");
        }

        public async Task InitializeDatabaseAsync()
        {
            if (await playersCollection.CountDocumentsAsync(FilterDefinition<PlayerEntity>.Empty) == 0)
            {
                await AddInitialPlayersAsync();
            }

            if (await teamsCollection.CountDocumentsAsync(FilterDefinition<TeamEntity>.Empty) == 0)
            {
                await AddInitialTeamsAsync();
            }
        }

        private async Task AddInitialPlayersAsync()
        {
            var players = new List<PlayerEntity>
            {
                new() { Id = 1, Name = "John Doe", Position = "Forward", Age = 25, TeamId = 1 },
                new() { Id = 2, Name = "Jane Smith", Position = "Goalkeeper", Age = 28, TeamId = 1 }
            };

            await playersCollection.InsertManyAsync(players);
        }

        private async Task AddInitialTeamsAsync()
        {
            var teams = new List<TeamEntity>
            {
                new TeamEntity { Id = 1, Name = "Team A", Location = "City A", Coach = "Coach A" },
                new TeamEntity { Id = 2, Name = "Team B", Location = "City B", Coach = "Coach B" }
            };

            await teamsCollection.InsertManyAsync(teams);
        }
    }
}
