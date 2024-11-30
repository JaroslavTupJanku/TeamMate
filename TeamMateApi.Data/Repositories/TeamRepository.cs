using MongoDB.Driver;
using TeamMateServer.Data.Entities;

namespace TeamMateServer.Data.Repositories
{
    public class TeamRepository : IRepository<TeamEntity>
    {
        private readonly IMongoCollection<TeamEntity> _teamsCollection;

        public TeamRepository(string databaseName, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _teamsCollection = database.GetCollection<TeamEntity>("Teams");
        }

        public async Task<List<TeamEntity>> GetAllAsync()
        {
            return await _teamsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<TeamEntity> GetByIdAsync(int id)
        {
            return await _teamsCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TeamEntity team)
        {
            await _teamsCollection.InsertOneAsync(team);
        }

        public async Task UpdateAsync(int id, TeamEntity team)
        {
            await _teamsCollection.ReplaceOneAsync(t => t.Id == id, team);
        }

        public async Task DeleteAsync(int id)
        {
            await _teamsCollection.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<List<TeamEntity>> SearchAsync(string text)
        {
            var filter = Builders<TeamEntity>.Filter.Text(text);
            return await _teamsCollection.Find(filter).ToListAsync();
        }

    }
}
