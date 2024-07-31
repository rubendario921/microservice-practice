using MongoDB.Driver;

namespace multitrabajos_history.Repositories
{
    public interface IMongoBookDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
