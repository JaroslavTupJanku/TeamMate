using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using TeamMateApi.Core.Enums;
using TeamMateServer.Data.Entities;

namespace TeamMateApi.Data
{
    public class MongoDBInitializer
    {
        private readonly IMongoCollection<PlayerEntity> playersCollection;
        private readonly IMongoCollection<TeamEntity> teamsCollection;
        private readonly ILogger<MongoDBInitializer> logger;

        public MongoDBInitializer(IMongoClient mongoClient, string databaseName, ILogger<MongoDBInitializer> logger)
        {
            var database = mongoClient.GetDatabase(databaseName);
            playersCollection = database.GetCollection<PlayerEntity>("Players");
            teamsCollection = database.GetCollection<TeamEntity>("Teams");
            this.logger = logger;
        }

        public async Task InitializeDatabaseAsync()
        {
            try
            {
                if (await playersCollection.EstimatedDocumentCountAsync() == 0)
                {
                    await AddInitialPlayersAsync();
                    logger.LogInformation("Initial players added to the database.");
                }

                if (await teamsCollection.EstimatedDocumentCountAsync() == 0)
                {
                    await AddInitialTeamsAsync();
                    logger.LogInformation("Initial teams added to the database.");
                }

                logger.LogInformation("Database initialization completed successfully.");
            }
            catch (MongoConnectionException ex)
            {
                logger.LogError(ex, "Error: Unable to connect to the MongoDB server. Please ensure that MongoDB is installed and running.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred during database initialization.");
            }
        }

        private async Task AddInitialPlayersAsync()
        {
            var players = new List<PlayerEntity>
            {
                new() { Id = 1, Name = "John", Surname="Doe", Position = Position.AttackingMidfielder, Age = 25, TeamId = 1 },
                new() { Id = 2, Name = "Jane", Surname="Smith", Position = Position.GoalKeeper, Age = 28, TeamId = 1 }
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
