using TeamMateServer.Data.Entities;
using TeamMateServer.Data.Repositories;

namespace TeamMateApi.Data.Repositories
{
    public interface IPlayerRepository : IRepository<PlayerEntity>
    {
        Task<List<PlayerEntity>> SearchAsync(string text);
    }
}
