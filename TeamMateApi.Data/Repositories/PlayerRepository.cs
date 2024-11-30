using MongoDB.Driver;
using TeamMateServer.Data.Entities;

namespace TeamMateServer.Data.Repositories
{
    public class PlayerRepository : IRepository<PlayerEntity>
    {
        private readonly IMongoCollection<PlayerEntity> _playersCollection;

        public PlayerRepository(string databaseName, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _playersCollection = database.GetCollection<PlayerEntity>("Players");

            var indexKeys = Builders<PlayerEntity>.IndexKeys.Text(p => p.Name).Text(p => p.Details);
            _playersCollection.Indexes.CreateOne(new CreateIndexModel<PlayerEntity>(indexKeys));
        }

        public async Task<List<PlayerEntity>> GetAllAsync()
        {
            return await _playersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<PlayerEntity> GetByIdAsync(int id)
        {
            return await _playersCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(PlayerEntity player)
        {
            await _playersCollection.InsertOneAsync(player);
        }

        public async Task UpdateAsync(int id, PlayerEntity player)
        {
            await _playersCollection.ReplaceOneAsync(p => p.Id == id, player);
        }

        public async Task DeleteAsync(int id)
        {
            await _playersCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<List<PlayerEntity>> SearchAsync(string text)
        {
            var filter = Builders<PlayerEntity>.Filter.Text(text);
            return await _playersCollection.Find(filter).ToListAsync();
        }
    }
}
