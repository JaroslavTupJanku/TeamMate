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
                await CreateTextIndexAsync();
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

        private async Task CreateTextIndexAsync()
        {
            var indexKeysDefinition = Builders<TeamEntity>.IndexKeys.Text(t => t.Name);
            var indexModel = new CreateIndexModel<TeamEntity>(indexKeysDefinition);

            await teamsCollection.Indexes.CreateOneAsync(indexModel);
            logger.LogInformation("Text index created on the 'Name' field in the Teams collection.");
        }

        private async Task AddInitialPlayersAsync()
        {
            var players = new List<PlayerEntity>
            {
                new() { Id = 1, Name = "John", Surname="Doe", Position = Position.AttackingMidfielder, Age = 25, TeamId = 1 },
                new() { Id = 2, Name = "Jane", Surname="Smith", Position = Position.GoalKeeper, Age = 28, TeamId = 1 },
                new() { Id = 3, Name = "Alice", Surname="Johnson", Position = Position.DefensiveMidfielder, Age = 22, TeamId = 2 },
                new() { Id = 4, Name = "Bob", Surname="Brown", Position = Position.CenterBack, Age = 27, TeamId = 2 },
                new() { Id = 5, Name = "Charlie", Surname="Davis", Position = Position.CenterForward, Age = 24, TeamId = 3 },
                new() { Id = 6, Name = "Emily", Surname="Wilson", Position = Position.AttackingMidfielder, Age = 21, TeamId = 3 },
                new() { Id = 7, Name = "Frank", Surname="Miller", Position = Position.LeftMidfielder, Age = 29, TeamId = 4 },
                new() { Id = 8, Name = "Grace", Surname="Taylor", Position = Position.GoalKeeper, Age = 26, TeamId = 4 },
                new() { Id = 9, Name = "Henry", Surname="Martinez", Position = Position.Striker, Age = 23, TeamId = 5 },
                new() { Id = 10, Name = "Isabel", Surname="Garcia", Position = Position.CentralMidfielder, Age = 20, TeamId = 5 },
                new() { Id = 11, Name = "Daniel", Surname="Anderson", Position = Position.LeftBack, Age = 30, TeamId = 1 },
                new() { Id = 12, Name = "Sophia", Surname="Martin", Position = Position.RightBack, Age = 24, TeamId = 2 },
                new() { Id = 13, Name = "Oliver", Surname="Clark", Position = Position.GoalKeeper, Age = 22, TeamId = 3 },
                new() { Id = 14, Name = "Emma", Surname="Lewis", Position = Position.CenterBack, Age = 27, TeamId = 4 },
                new() { Id = 15, Name = "Liam", Surname="Walker", Position = Position.AttackingMidfielder, Age = 28, TeamId = 5 }
            };

            await playersCollection.InsertManyAsync(players);
        }

        private async Task AddInitialTeamsAsync()
        {
            var teams = new List<TeamEntity>
            {
                new () { Id = 1, Name = "Team A", Location = "City A", Coach = "Coach A" },
                new () { Id = 2, Name = "Team B", Location = "City B", Coach = "Coach B" },
                new () { Id = 3, Name = "Team C", Location = "City C", Coach = "Coach C" },
                new () { Id = 4, Name = "Team D", Location = "City D", Coach = "Coach D" },
                new () { Id = 5, Name = "Team E", Location = "City E", Coach = "Coach E" },
            };

            await teamsCollection.InsertManyAsync(teams);
        }
    }
}
